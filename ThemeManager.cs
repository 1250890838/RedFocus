using System.Diagnostics;
using System.Windows;

namespace RedFocus
{
    public enum Theme
    {
        Dark,
        Light,
        Blue
    }

    public static class ThemeManager
    {
        private static Theme _currentTheme = Theme.Dark;

        public static Theme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    ApplyTheme(value);
                }
            }
        }

        public static void ApplyTheme(Theme theme)
        {
            var themePath = theme switch
            {
                Theme.Dark => "Themes/DarkTheme.xaml",
                Theme.Light => "Themes/LightTheme.xaml",
                Theme.Blue => "Themes/BlueTheme.xaml",
                _ => "Themes/DarkTheme.xaml"
            };

            var existingTheme = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source?.OriginalString?.Contains("Theme.xaml") == true);

            if (existingTheme != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(existingTheme);
            }

            var newTheme = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries.Add(newTheme);

            _currentTheme = theme;
        }

        public static void Initialize()
        {
            ApplyTheme(_currentTheme);
        }
    }
}
