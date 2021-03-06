﻿using System.Windows;
using System.Windows.Shapes;
using Infrustructure.Plans.Elements;

namespace Infrustructure.Plans.Painters
{
	public class ShapePainter<T> : IPainter
		where T : Shape, new()
	{
		protected T CreateShape(ElementBase element)
		{
			T shape = new T();
			PainterHelper.SetStyle(shape, element);
			//shape.IsHitTestVisible = false;
			return shape;
		}

		public virtual FrameworkElement Draw(ElementBase element)
		{
			return CreateShape(element);
		}
	}
}
