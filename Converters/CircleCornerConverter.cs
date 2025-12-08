using System.Windows;
using System.Windows.Data;

namespace RedFocus.Converters
{
    public class CircleCornerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return new CornerRadius(doubleValue / 2);
            }
            throw new InvalidOperationException("Invalid input for CircleCornetConverter");
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
