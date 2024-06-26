﻿using Lib;
using System;
using System.Windows;

namespace WpfLib.ValueConverters
{
    public class NotEmptyVisibilityConverter : BaseConverter<NotEmptyVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.NullableToStr() == string.Empty)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

    }
}
