﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Infrustructure.Plans.Designer
{
	public abstract class InstrumentAdorner : Adorner
	{
		private VisualCollection visuals;
		protected Point? StartPoint { get; set; }
		protected CommonDesignerCanvas DesignerCanvas { get; private set; }
		protected Canvas AdornerCanvas { get; private set; }

		protected override int VisualChildrenCount
		{
			get { return visuals.Count; }
		}
		protected override Size ArrangeOverride(Size arrangeBounds)
		{
			AdornerCanvas.Arrange(new Rect(arrangeBounds));
			return arrangeBounds;
		}
		protected override Visual GetVisualChild(int index)
		{
			return visuals[index];
		}

		protected double ZoomFactor
		{
			get { return DesignerCanvas.Zoom; }
		}

		public InstrumentAdorner(CommonDesignerCanvas designerCanvas)
			: base(designerCanvas)
		{
			DesignerCanvas = designerCanvas;

			AdornerCanvas = new Canvas()
			{
				Background = Brushes.Transparent
			};
			visuals = new VisualCollection(this);
			visuals.Add(AdornerCanvas);
		}

		public void Show(Point? startPoint)
		{
			StartPoint = startPoint;
			AdornerCanvas.Children.Clear();
			Show();
			AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(DesignerCanvas);
			if (adornerLayer != null)
					adornerLayer.Add(this);
		}
		protected abstract void Show();
		public void Hide()
		{
			if (IsMouseCaptured)
				ReleaseMouseCapture();

			AdornerLayer adornerLayer = Parent as AdornerLayer;
			if (adornerLayer != null)
				adornerLayer.Remove(this);
		}

		public virtual void UpdateZoom()
		{
		}
		public virtual void KeyboardInput(Key key)
		{
		}

		protected Point CutPoint(Point point)
		{
			var result = new Point(point.X, point.Y);
			if (result.X < 0)
				result = new Point(0, result.Y);
			if (result.Y < 0)
				result = new Point(result.X, 0);
			if (result.X > AdornerCanvas.ActualWidth)
				result = new Point(AdornerCanvas.ActualWidth, result.Y);
			if (result.Y > AdornerCanvas.ActualHeight)
				result = new Point(result.X, AdornerCanvas.ActualHeight);
			return result;
		}
	}
}
