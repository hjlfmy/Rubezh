﻿using System;
using System.Windows.Data;

namespace Controls
{
    public class StateTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return FiresecAPI.Models.EnumsConverter.StateTypeToClassName((FiresecAPI.Models.StateType) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (FiresecAPI.Models.StateType) value;
            //throw new NotImplementedException();
        }
    }
}