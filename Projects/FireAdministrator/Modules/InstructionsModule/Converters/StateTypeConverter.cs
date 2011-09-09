﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using FiresecAPI.Models;
using System.IO;
using System.Windows;

namespace InstructionsModule.Converters
{
    class StateTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string Value = (string)value;
            if (Value == "All")
            {
                return "Показать все";
            }
            else
            {
                StateType stateType = new StateType();
                foreach (StateType stType in Enum.GetValues(typeof(StateType)))
                {
                    if ((Enum.GetName(typeof(StateType), stType) == Value))
                    {
                        stateType = stType;
                    }
                }
                return FiresecAPI.Models.EnumsConverter.StateTypeToClassName(stateType);
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

    }
}