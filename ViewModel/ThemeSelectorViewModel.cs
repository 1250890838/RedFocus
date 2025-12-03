using RedFocus.Controls;
using RedFocus.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace RedFocus.ViewModel;
class ThemeSelectorViewModel
{
    public List<ThemeItem> Themes { get; set; } = new List<ThemeItem>();
    public ThemeSelectorViewModel()
    {
        var themeUris = new Dictionary<string, string>
            {
                {"Dark Theme", "/Themes/DarkTheme.xaml"},
                {"Blue Theme", "/Themes/BlueTheme.xaml"},
                {"Light Theme", "/Themes/LightTheme.xaml"},
            };
        LoadThemes(themeUris);
    }
    public ICommand ApplyThemeCommand => new RelayCommand(ApplyTheme);

    private void LoadThemes(Dictionary<string, string> uris)
    {
        foreach (var kvp in uris)
        {
            var resourceDict = new ResourceDictionary
            {
                Source = new Uri(kvp.Value, UriKind.RelativeOrAbsolute)
            };
            Brush brush = Brushes.Black;
            if (resourceDict.Contains("PrimaryBackgroundBrush") &&
                resourceDict["PrimaryBackgroundBrush"] is SolidColorBrush primaryBackgroundBrush)
            {
                brush = primaryBackgroundBrush;
            }
            var item = new ThemeItem
            {
                Name = kvp.Key,
                ResourceUri = kvp.Value,
                PrimaryColor = brush,
                IsSelected = false
            };
            if (resourceDict.Contains("AccentBrush") &&
                resourceDict["AccentBrush"] is SolidColorBrush accentBrush)
            {
                brush = accentBrush;
            }
            item.AccentColor = brush;
            Themes.Add(item);
        }
        Themes[0].IsSelected = true;
    }

    public void ApplyTheme(object? uri)
    {
        if(uri is not string resourceUri)
        {
            return;
        }
        var existingTheme = Application.Current.Resources.MergedDictionaries
            .FirstOrDefault(d => d.Source?.OriginalString?.Contains("Theme.xaml") == true);

        if (existingTheme != null)
        {
            Application.Current.Resources.MergedDictionaries.Remove(existingTheme);
        }

        var newTheme = new ResourceDictionary
        {
            Source = new Uri(resourceUri, UriKind.Relative)
        };
        Application.Current.Resources.MergedDictionaries.Add(newTheme);

        foreach (var item in Themes)
        {
            if (item.ResourceUri != resourceUri)
                item.IsSelected = false;
        }
    }
}
