﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using DeviceControls;
using FiresecAPI.Models;
using FiresecClient;
using System.Windows.Shapes;
using Infrastructure.Common;
using PlansModule.ViewModels;
using Infrastructure;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using FiresecAPI;
using PlansModule.Events;

namespace PlansModule.Designer
{
    public class DesignerCanvas : Canvas
    {
        public Plan Plan { get; set; }
        public PlanDesignerViewModel PlanDesignerViewModel { get; set; }
        private Point? dragStartPoint = null;

        public DesignerCanvas()
        {
            AllowDrop = true;
            Background = new SolidColorBrush(Colors.DarkGray);
            Width = 100;
            Height = 100;

            ShowPropertiesCommand = new RelayCommand(OnShowProperties);
            PreviewMouseDown += new MouseButtonEventHandler(On_PreviewMouseDown);
            DataContext = this;
        }

        public IEnumerable<DesignerItem> Items
        {
            get
            {
                return from item in this.Children.OfType<DesignerItem>() select item;
            }
        }

        public IEnumerable<DesignerItem> SelectedItems
        {
            get
            {
                return from item in this.Children.OfType<DesignerItem>()
                    where item.IsSelected == true
                    select item;
            }
        }

        public void DeselectAll()
        {
            foreach (DesignerItem item in this.SelectedItems)
            {
                item.IsSelected = false;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                this.dragStartPoint = new Point?(e.GetPosition(this));
                this.DeselectAll();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                this.dragStartPoint = null;
            }

            if (this.dragStartPoint.HasValue)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    RubberbandAdorner adorner = new RubberbandAdorner(this, this.dragStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }

                e.Handled = true;
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            DesignerItemData designerItemData = e.Data.GetData("DESIGNER_ITEM") as DesignerItemData;
            var elementBase = designerItemData.ElementBase as ElementBase;

            Point position = e.GetPosition(this);
            elementBase.Left = Math.Max(0, position.X - elementBase.Width / 2);
            elementBase.Top = Math.Max(0, position.Y - elementBase.Height / 2);

            var designerItem = AddElementBase(elementBase);

            this.DeselectAll();
            designerItem.IsSelected = true;
            PlanDesignerViewModel.MoveToFront();
            e.Handled = true;

            ServiceFactory.Events.GetEvent<ElementPositionChangedEvent>().Publish(null);
        }

        public DesignerItem AddElementBase(ElementBase elementBase)
        {
            if (elementBase is ElementRectangle)
            {
                Plan.ElementRectangles.Add(elementBase as ElementRectangle);
            }
            if (elementBase is ElementEllipse)
            {
                Plan.ElementEllipses.Add(elementBase as ElementEllipse);
            }
            if (elementBase is ElementPolygon)
            {
                Plan.ElementPolygons.Add(elementBase as ElementPolygon);
            }
            if (elementBase is ElementTextBlock)
            {
                Plan.ElementTextBlocks.Add(elementBase as ElementTextBlock);
            }
            if (elementBase is ElementDevice)
            {
                Plan.ElementDevices.Add(elementBase as ElementDevice);
            }

            DesignerItem designerItem = null;
            if (elementBase is ElementDevice)
            {
                var elementDevice = elementBase as ElementDevice;
                var device = FiresecManager.DeviceConfiguration.Devices.FirstOrDefault(x => x.UID == elementDevice.DeviceUID);
                var devicePicture = DeviceControl.GetDefaultPicture(device.Driver.UID);
                designerItem = Create(elementDevice, frameworkElement: devicePicture);
            }
            else
            {
                designerItem = Create(elementBase);
            }
            return designerItem;
        }

        public DesignerItem Create(ElementBase elementBase, FrameworkElement frameworkElement = null)
        {
            if (elementBase is ElementPolygonZone)
            {
                ElementPolygonZone elementPolygonZone = elementBase as ElementPolygonZone;
                elementPolygonZone.BackgroundColor = GetZoneColor(elementPolygonZone.ZoneNo);
            }
            if (elementBase is ElementRectangleZone)
            {
                ElementRectangleZone elementRectangleZone = elementBase as ElementRectangleZone;
                elementRectangleZone.BackgroundColor = GetZoneColor(elementRectangleZone.ZoneNo);
            }

            if (frameworkElement == null)
            {
                frameworkElement = elementBase.Draw();
            }
            frameworkElement.IsHitTestVisible = false;

            var designerItem = new DesignerItem()
            {
                MinWidth = 10,
                MinHeight = 10,
                Width = elementBase.Width,
                Height = elementBase.Height,
                Content = frameworkElement,
                ElementBase = elementBase,
                IsPolygon = elementBase is ElementBasePolygon
            };

            if (elementBase is ElementPolygonZone)
                designerItem.Opacity = 0.5;
            if (elementBase is ElementRectangleZone)
                designerItem.Opacity = 0.5;

            DesignerCanvas.SetLeft(designerItem, elementBase.Left);
            DesignerCanvas.SetTop(designerItem, elementBase.Top);
            this.Children.Add(designerItem);

            if (elementBase is IZIndexedElement)
                Panel.SetZIndex(designerItem, (elementBase as IZIndexedElement).ZIndex);
            if (elementBase is ElementDevice)
                Panel.SetZIndex(designerItem, 200000);
            if (elementBase is ElementPolygonZone)
                Panel.SetZIndex(designerItem, 100000);
            if (elementBase is ElementRectangleZone)
                Panel.SetZIndex(designerItem, 100000);

            return designerItem;
        }

        Color GetZoneColor(ulong? zoneNo)
        {
            Color color = Colors.Gray;
            if (zoneNo.HasValue)
            {
                var zone = FiresecManager.DeviceConfiguration.Zones.FirstOrDefault(x => x.No == zoneNo.Value);
                if (zone != null)
                {
                    if (zone.ZoneType == ZoneType.Fire)
                        color = Colors.Green;

                    if (zone.ZoneType == ZoneType.Guard)
                        color = Colors.Brown;
                }
            }
            return color;
        }

        public bool IsPointAdding = false;

        void On_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsPointAdding)
            {
                if (SelectedItems == null)
                    return;
                var selectedItem = SelectedItems.FirstOrDefault();
                if (selectedItem == null)
                    return;
                if (selectedItem.IsPolygon == false)
                    return;

                IsPointAdding = false;

                var item = selectedItem;
                var polygon = item.Content as Polygon;


                Point currentPoint = e.GetPosition(selectedItem);
                var minDistance = double.MaxValue;
                int minIndex = 0;
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    var polygonPoint = polygon.Points[i];
                    var distance = Math.Pow(currentPoint.X - polygonPoint.X, 2) + Math.Pow(currentPoint.Y - polygonPoint.Y, 2);
                    if (distance < minDistance)
                    {
                        minIndex = i;
                        minDistance = distance;
                    }
                }
                minIndex = minIndex + 1;
                Point point = e.GetPosition(selectedItem);
                polygon.Points.Insert(minIndex, point);

                PolygonResizeChrome.Current.Initialize();

                e.Handled = true;
            }
        }

        public RelayCommand ShowPropertiesCommand { get; private set; }
        void OnShowProperties()
        {
            var designerPropertiesViewModel = new DesignerPropertiesViewModel(Plan);
            if (ServiceFactory.UserDialogs.ShowModalWindow(designerPropertiesViewModel))
                {
                    Update();
                    PlansModule.HasChanges = true;
                }
        }

        public void Update()
        {
            Background = new SolidColorBrush(Plan.BackgroundColor);

            if (Plan.BackgroundPixels != null)
            {
                Background = PlanElementsHelper.CreateBrush(Plan.BackgroundPixels);
            }
        }
    }
}