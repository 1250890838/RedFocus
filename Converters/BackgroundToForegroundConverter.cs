using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace RedFocus.Converters;
public class BackgroundToForegroundConverter : IValueConverter
{
    private const int LuminosityThreshold = 149;
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Color backgroundColor;
        if (value is SolidColorBrush brush)
        {
            backgroundColor = brush.Color;
        }
        else if (value is Color color)
        {
            backgroundColor = color;
        }
        else
        {
            return Brushes.Black;
        }
        double luminosity = (0.299 * backgroundColor.R +
                             0.587 * backgroundColor.G +
                             0.114 * backgroundColor.B);
        if (luminosity > LuminosityThreshold)
        {
            return Brushes.Black;
        }
        else
        {
            return Brushes.White;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
