using Lib;
using System;

namespace WpfLib.ValueConverters
{
    public class DateFormatConverter : BaseConverter<DateFormatConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (value == null) return null;
            //return DateTime.Parse(value.NullableToStr());
            if (value.NullableToStr() == string.Empty) return null;
            return DateTime.Parse(value.NullableToStr());
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (value == null) return null;
            //return ((DateTime)value).ToString("yyyy/MM/dd");
            if (value.NullableToStr() == string.Empty) return string.Empty;
            return ((DateTime)value).ToString("yyyy/MM/dd");
        }
    }
}
