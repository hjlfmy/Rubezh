﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Infrastructure;
using Infrustructure.Plans.Designer;

namespace PlansModule.Designer
{
	public class MoveThumb : Thumb
	{
		private bool _wasMoved;
		private DesignerItem DesignerItem
		{
			get { return DataContext as DesignerItem; }
		}
		private DesignerCanvas DesignerCanvas
		{
			get { return VisualTreeHelper.GetParent(DesignerItem) as DesignerCanvas; }
		}

		public MoveThumb()
		{
			Initialized += new EventHandler(MoveThumb_Initialized);
			DragStarted += new DragStartedEventHandler(this.MoveThumb_DragStarted);
			DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
			DragCompleted += new DragCompletedEventHandler(MoveThumb_DragCompleted);
		}

		void MoveThumb_Initialized(object sender, EventArgs e)
		{
		}

		private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
		{
			_wasMoved = false;
			DesignerCanvas.BeginChange();
		}
		private void MoveThumb_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			if (_wasMoved)
				DesignerCanvas.EndChange();
		}
		private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			if (DesignerItem.IsSelected)
			{
				_wasMoved = true;
				Vector shift = new Vector(e.HorizontalChange, e.VerticalChange);
				foreach (DesignerItem designerItem in DesignerCanvas.SelectedItems)
				{
					Rect rect = new Rect(Canvas.GetLeft(designerItem), Canvas.GetTop(designerItem), designerItem.ActualWidth, designerItem.ActualHeight);
					if (rect.Right + shift.X > DesignerCanvas.Width)
						shift.X = DesignerCanvas.Width - rect.Right;
					if (rect.Left + shift.X < 0)
						shift.X = -rect.Left;
					if (rect.Bottom + shift.Y > DesignerCanvas.Height)
						shift.Y = DesignerCanvas.Height - rect.Bottom;
					if (rect.Top + shift.Y < 0)
						shift.Y = -rect.Top;
				}
				foreach (DesignerItem designerItem in DesignerCanvas.SelectedItems)
				{
					designerItem.Element.Position += shift;
					designerItem.SetLocation();
				}
				DesignerCanvas.InvalidateMeasure();
				e.Handled = true;
				ServiceFactory.SaveService.PlansChanged = true;
			}
		}

		protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
		{
			base.OnPreviewMouseDown(e);
		}
	}
}