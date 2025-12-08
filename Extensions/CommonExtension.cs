using System.Windows.Media;

namespace RedFocus.Extensions;
public static class ColorExtension
{
    public static Color Lighten(this Color color, double factor = 0.2)
    {
        return Color.FromArgb(
            color.A,
            (byte)(color.R + (255 - color.R) * factor),
            (byte)(color.G + (255 - color.G) * factor),
            (byte)(color.B + (255 - color.B) * factor));
    }
    public static Color Darken(this Color color, double factor = 0.2)
    {
        return Color.FromArgb(
            color.A,
            (byte)(color.R * (1 - factor)),
            (byte)(color.G * (1 - factor)),
            (byte)(color.B * (1 - factor)));
    }
}
