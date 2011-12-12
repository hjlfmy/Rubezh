﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FiresecAPI;
using FiresecAPI.Models;
using Infrastructure;
using Infrastructure.Common;
using PlansModule.Events;
using PlansModule.Views;

namespace PlansModule.ViewModels
{
    public class PlanCanvasViewModel : RegionViewModel
    {
        Plan _plan;
        Canvas _canvas;

        public List<ElementSubPlanViewModel> SubPlans { get; set; }
        public List<ElementZoneViewModel> Zones { get; set; }
        public List<ElementDeviceViewModel> Devices { get; set; }

        public PlanCanvasViewModel(Canvas canvas)
        {
            //_canvas = canvas;
            //_canvas.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(_canvas_PreviewMouseLeftButtonDown);
            ServiceFactory.Events.GetEvent<PlanStateChangedEvent>().Subscribe(OnPlanStateChanged);
            ServiceFactory.Events.GetEvent<ElementDeviceSelectedEvent>().Subscribe(OnElementDeviceSelected);
            ServiceFactory.Events.GetEvent<ElementZoneSelectedEvent>().Subscribe(OnElementZoneSelected);
        }

        public void Initialize(Plan plan)
        {
            _plan = plan;
            DrawPlan();
            UpdateSubPlans();
            ResetView();
        }

        public void DrawPlan()
        {
            Canvas newCanvas = new Canvas();
            
            _canvas = newCanvas;

            SubPlans = new List<ElementSubPlanViewModel>();
            Zones = new List<ElementZoneViewModel>();
            Devices = new List<ElementDeviceViewModel>();

            _canvas.Children.Clear();
            _canvas.Width = _plan.Width;
            _canvas.Height = _plan.Height;
            _canvas.Background = new SolidColorBrush(_plan.BackgroundColor);
            if (_plan.BackgroundPixels != null)
            {
                _canvas.Background = PlanElementsHelper.CreateBrush(_plan.BackgroundPixels);
            }

            foreach (var elementRectangle in _plan.ElementRectangles)
            {
                DrawElement(elementRectangle);
            }
            foreach (var elementEllipse in _plan.ElementEllipses)
            {
                DrawElement(elementEllipse);
            }
            foreach (var elementTextBlock in _plan.ElementTextBlocks)
            {
                DrawElement(elementTextBlock);
            }
            foreach (var elementPolygon in _plan.ElementPolygons)
            {
                DrawElement(elementPolygon);
            }
            foreach (var elementSubPlan in _plan.ElementSubPlans)
            {
                var subPlanViewModel = new ElementSubPlanViewModel(elementSubPlan);
                DrawElement(subPlanViewModel.ElementSubPlanView, elementSubPlan, subPlanViewModel);
                SubPlans.Add(subPlanViewModel);
            }

            foreach (var elementRectangleZone in _plan.ElementRectangleZones)
            {
                if (elementRectangleZone.ZoneNo != null)
                {
                    var elementPolygonZone = RectangleZoneToPolygon(elementRectangleZone);
                    var elementZoneViewModel = new ElementZoneViewModel(elementPolygonZone);
                    DrawElement(elementZoneViewModel.ElementZoneView, elementRectangleZone, elementZoneViewModel);
                    Zones.Add(elementZoneViewModel);
                }
            }

            foreach (var elementPolygonZone in _plan.ElementPolygonZones)
            {
                if (elementPolygonZone.ZoneNo != null)
                {
                    var elementZoneViewModel = new ElementZoneViewModel(elementPolygonZone);
                    DrawElement(elementZoneViewModel.ElementZoneView, elementPolygonZone, elementZoneViewModel);
                    Zones.Add(elementZoneViewModel);
                }
            }

            foreach (var elementDevice in _plan.ElementDevices)
            {
                //continue;
                var elementDeviceViewModel = new ElementDeviceViewModel(elementDevice);
                DrawElement(elementDeviceViewModel.ElementDeviceView, elementDevice, elementDeviceViewModel);
                Devices.Add(elementDeviceViewModel);
            }

            PlansViewModel.Current.MainCanvas = newCanvas;
        }

        void DrawElement(ElementBase elementBase)
        {
            var frameworkElement = elementBase.Draw();
            frameworkElement.Width = elementBase.Width;
            frameworkElement.Height = elementBase.Height;
            Canvas.SetLeft(frameworkElement, elementBase.Left);
            Canvas.SetTop(frameworkElement, elementBase.Top);
            _canvas.Children.Add(frameworkElement);
        }

        void DrawElement(UserControl userControl, ElementBase elementBase, BaseViewModel elementViewModel)
        {
            userControl.DataContext = elementViewModel;
            userControl.Width = elementBase.Width;
            userControl.Height = elementBase.Height;
            Canvas.SetLeft(userControl, elementBase.Left);
            Canvas.SetTop(userControl, elementBase.Top);
            _canvas.Children.Add(userControl);
        }

        ElementPolygonZone RectangleZoneToPolygon(ElementRectangleZone elementRectangleZone)
        {
            var elementPolygonZone = new ElementPolygonZone()
            {
                Left = elementRectangleZone.Left,
                Top = elementRectangleZone.Top,
                Width = elementRectangleZone.Width,
                Height = elementRectangleZone.Height,
                ZoneNo = elementRectangleZone.ZoneNo
            };

            elementPolygonZone.PolygonPoints = new PointCollection();
            elementPolygonZone.PolygonPoints.Add(new Point(0, 0));
            elementPolygonZone.PolygonPoints.Add(new Point(elementRectangleZone.Width, 0));
            elementPolygonZone.PolygonPoints.Add(new Point(elementRectangleZone.Width, elementRectangleZone.Height));
            elementPolygonZone.PolygonPoints.Add(new Point(0, elementRectangleZone.Height));

            return elementPolygonZone;
        }

        void OnElementDeviceSelected(object obj)
        {
            Devices.ForEach(x => x.IsSelected = false);
        }

        void OnElementZoneSelected(object obj)
        {
            Zones.ForEach(x => x.IsSelected = false);
        }

        void _canvas_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Devices.ForEach(x => x.IsSelected = false);
            Zones.ForEach(x => x.IsSelected = false);
        }

        public void SelectDevice(Guid deviceUID)
        {
            Devices.ForEach(x => x.IsSelected = false);
            Devices.FirstOrDefault(x => x.DeviceUID == deviceUID).IsSelected = true;
        }

        public void SelectZone(ulong zoneNo)
        {
            Zones.ForEach(x => x.IsSelected = false);
            Zones.FirstOrDefault(x => x.ZoneNo.Value == zoneNo).IsSelected = true;
        }

        void OnPlanStateChanged(Guid planUID)
        {
            if ((_plan != null) && (_plan.UID == planUID))
            {
                UpdateSubPlans();
            }
        }

        void UpdateSubPlans()
        {
            foreach (var subPlan in SubPlans)
            {
                var planViewModel = PlansViewModel.Current.Plans.FirstOrDefault(x => x.Plan.UID == subPlan.PlanUID);
                if (planViewModel != null)
                {
                    subPlan.StateType = planViewModel.StateType;
                }
            }
        }

        void ResetView()
        {
            if (CanvasView.Current != null)
            {
                CanvasView.Current.Reset();
            }
        }
    }
}