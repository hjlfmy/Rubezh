﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Infrustructure.Plans.Designer;
using Infrustructure.Plans.Elements;

namespace PlansModule.InstrumentAdorners
{
	public abstract class BaseRectangleAdorner : InstrumentAdorner
	{
		private Point? endPoint;
		private Shape rubberband;

		public BaseRectangleAdorner(CommonDesignerCanvas designerCanvas)
			: base(designerCanvas)
		{
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
			return new Rectangle();
		}
		protected abstract ElementBaseRectangle CreateElement();

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				StartPoint = e.GetPosition(this);
				endPoint = null;
				if (!AdornerCanvas.Children.Contains(rubberband))
					AdornerCanvas.Children.Add(rubberband);
				//AdornerCanvas.Cursor = null;
				if (!AdornerCanvas.IsMouseCaptured)
					AdornerCanvas.CaptureMouse();
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed && AdornerCanvas.Children.Contains(rubberband))
			{
				if (!AdornerCanvas.IsMouseCaptured)
					AdornerCanvas.CaptureMouse();
				endPoint = CutPoint(e.GetPosition(this));
				UpdateRubberband();
				e.Handled = true;
			}
		}
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			if (AdornerCanvas.IsMouseCaptured && StartPoint.HasValue)
			{
				AdornerCanvas.ReleaseMouseCapture();
				AdornerCanvas.Children.Remove(rubberband);
				//AdornerCanvas.Cursor = Cursors.Pen;
				ElementBaseRectangle element = CreateElement();
				if (element != null)
				{
					if (endPoint.HasValue)
					{
						element.Left = Canvas.GetLeft(rubberband);
						element.Top = Canvas.GetTop(rubberband);
						element.Height = rubberband.Height;
						element.Width = rubberband.Width;
					}
					else
						element.Position = StartPoint.Value;
					DesignerCanvas.CreateDesignerItem(element);
				}
				StartPoint = null;
			}
		}

		private void UpdateRubberband()
		{
			rubberband.StrokeThickness = 1 / ZoomFactor;

			double left = Math.Min(StartPoint.Value.X, endPoint.Value.X);
			double top = Math.Min(StartPoint.Value.Y, endPoint.Value.Y);

			double width = Math.Abs(StartPoint.Value.X - endPoint.Value.X);
			double height = Math.Abs(StartPoint.Value.Y - endPoint.Value.Y);

			rubberband.Width = width;
			rubberband.Height = height;
			Canvas.SetLeft(rubberband, left);
			Canvas.SetTop(rubberband, top);
		}
	}
}