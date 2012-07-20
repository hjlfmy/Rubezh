﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Infrustructure.Plans;
using Infrustructure.Plans.Designer;
using PlansModule.Designer;
using Infrustructure.Plans.Elements;
using FiresecAPI.Models;

namespace PlansModule.InstrumentAdorners
{
	public class PolygonAdorner : InstrumentAdorner
	{
		private Shape rubberband;

		public PolygonAdorner(DesignerCanvas designerCanvas)
			: base(designerCanvas)
		{
		}

		protected Shape Rubberband
		{
			get { return rubberband; }
		}

		protected override void Show()
		{
			rubberband = CreateRubberband();
			rubberband.Stroke = Brushes.Navy;
			rubberband.StrokeThickness = 1 / ZoomFactor;
			AdornerCanvas.Cursor = Cursors.Pen;
		}

		protected virtual Shape CreateRubberband()
		{
			return new Polygon();
		}
		protected virtual PointCollection Points
		{
			get { return ((Polygon)rubberband).Points; }
		}
		protected virtual ElementBaseShape CreateElement()
		{
			return new ElementPolygon();
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
			{
				if (!AdornerCanvas.Children.Contains(rubberband))
					AdornerCanvas.Children.Add(rubberband);
				if (!AdornerCanvas.IsMouseCaptured)
					AdornerCanvas.CaptureMouse();
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (AdornerCanvas.IsMouseCaptured)
			{
				Points[Points.Count - 1] = CutPoint(e.GetPosition(this));
				e.Handled = true;
			}
		}
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			if (AdornerCanvas.IsMouseCaptured && e.ButtonState == MouseButtonState.Released)
				switch (e.ChangedButton)
				{
					case MouseButton.Left:
						Points.Add(CutPoint(e.GetPosition(this)));
						if (Points.Count == 1)
							Points.Add(CutPoint(e.GetPosition(this)));
						break;
					case MouseButton.Right:
						AdornerCanvas.ReleaseMouseCapture();
						AdornerCanvas.Children.Remove(rubberband);
						ElementBaseShape element = CreateElement();
						if (element != null)
						{
							if (Points.Count > 1)
								element.Points = Points;
							else
								element.Position = CutPoint(e.GetPosition(this));
							((DesignerCanvas)DesignerCanvas).CreateDesignerItem(element);
						}
						break;
				}
		}
	}
}