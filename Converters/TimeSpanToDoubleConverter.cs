using System.Globalization;
using System.Windows.Data;

namespace RedFocus.Converters;

public class TimeSpanToDoubleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is TimeSpan ts ? ts.TotalMinutes : 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return TimeSpan.FromMinutes((double)value);
    }
}

public class IntToTimeSpanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is int minutes ? TimeSpan.FromMinutes(minutes) : TimeSpan.Zero;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is TimeSpan ts ? (int)ts.TotalMinutes : 0;
    }
}
