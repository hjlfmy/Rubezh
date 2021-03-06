﻿using System.Windows.Media;
using FiresecAPI.Models;
using Infrastructure.Common.Windows.ViewModels;

namespace PlansModule.ViewModels
{
	public class EllipsePropertiesViewModel : SaveCancelDialogViewModel
	{
		ElementEllipse _elementEllipse;
		public ImagePropertiesViewModel ImagePropertiesViewModel { get; private set; }

		public EllipsePropertiesViewModel(ElementEllipse elementEllipse)
		{
			Title = "Свойства фигуры: Эллипс";
			ImagePropertiesViewModel = new ImagePropertiesViewModel();
			_elementEllipse = elementEllipse;
			CopyProperties();
		}

		void CopyProperties()
		{
			BackgroundColor = _elementEllipse.BackgroundColor;
			BorderColor = _elementEllipse.BorderColor;
			StrokeThickness = _elementEllipse.BorderThickness;
			ImagePropertiesViewModel.BackgroundPixels = _elementEllipse.BackgroundPixels;
			ImagePropertiesViewModel.UpdateImage();
		}

		Color _backgroundColor;
		public Color BackgroundColor
		{
			get { return _backgroundColor; }
			set
			{
				_backgroundColor = value;
				OnPropertyChanged("BackgroundColor");
			}
		}

		Color _borderColor;
		public Color BorderColor
		{
			get { return _borderColor; }
			set
			{
				_borderColor = value;
				OnPropertyChanged("BorderColor");
			}
		}

		double _strokeThickness;
		public double StrokeThickness
		{
			get { return _strokeThickness; }
			set
			{
				_strokeThickness = value;
				OnPropertyChanged("StrokeThickness");
			}
		}

		protected override bool Save()
		{
			_elementEllipse.BackgroundColor = BackgroundColor;
			_elementEllipse.BorderColor = BorderColor;
			_elementEllipse.BorderThickness = StrokeThickness;
			_elementEllipse.BackgroundPixels = ImagePropertiesViewModel.BackgroundPixels;
			return base.Save();
		}
	}
}
