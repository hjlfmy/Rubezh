﻿using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Controls
{
    public class BrightnessToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte brightness = (byte) value;

            return Color.FromArgb(brightness, 255, 255, 255);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}