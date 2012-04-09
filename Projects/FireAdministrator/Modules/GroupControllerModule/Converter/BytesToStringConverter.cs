﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace GroupControllerModule.Converters
{
    public class BytesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var bytesValue = (List<byte>)value;
            var stringValue = new StringBuilder();
            foreach (var b in bytesValue)
            {
                stringValue.Append(b.ToString("x2") + " ");
            }
            return stringValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}