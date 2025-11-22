using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace RedFocus.Converters;

class TimeSpanToMMSSConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string showType = (string)parameter;
        if (showType == null)
        {
            if (value is TimeSpan timeSpan)
            {
                return $"{((int)Math.Floor(timeSpan.TotalMinutes)):D2}:{timeSpan.Seconds:D2}";
            }
        }
        else if(showType.Equals("Pure"))
        {
            return value is TimeSpan timeSpan
                ? $"{((int)Math.Floor((double)timeSpan.Minutes)):D2}"
                : "0";
        }
        throw new NotImplementedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
