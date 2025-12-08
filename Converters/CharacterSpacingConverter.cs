using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace RedFocus.Converters
{
    public sealed class CharacterSpacingConverter : IValueConverter
    {
        private const string ThinSpace = "\u200A"; // Hair space

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value?.ToString() ?? string.Empty;
            if (text.Length <= 1) return text;

            int count = 1;
            if (parameter is string p && int.TryParse(p, out var n) && n > 0) count = n;

            var spacer = new string(ThinSpace[0], count);
            var sb = new StringBuilder(text.Length * (count + 1));
            for (int i = 0; i < text.Length; i++)
            {
                sb.Append(text[i]);
                if (i < text.Length - 1) sb.Append(spacer);
            }
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value; // one-way
    }
}