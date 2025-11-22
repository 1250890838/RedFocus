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
        if(value is TimeSpan timeSpan)
        {
            return $"{((int)Math.Floor(timeSpan.TotalMinutes)):D2}:{timeSpan.Seconds:D2}";
        }
        return "00:00";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
