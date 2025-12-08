using RedFocus.Model;
using RedFocus.Properties;
using RedFocus.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace RedFocus.ViewModel;

internal class ThemeSelectorViewModel : ViewModelBase
{
    public ObservableCollection<ThemeItem> Themes { get; set; } = new();

    public ThemeSelectorViewModel()
    {
        var themeUris = new Dictionary<string, (string Uri, string Key)>
        {
        {"Dark", ("/Themes/DarkTheme.xaml", "Theme_Dark")},
     {"Blue", ("/Themes/BlueTheme.xaml", "Theme_Blue")},
       {"Light", ("/Themes/LightTheme.xaml", "Theme_Light")},
        };

        LoadThemes(themeUris);

        // 订阅语言变更事件
        LanguageService.Instance.LanguageChanged += OnLanguageChanged;
    }

    public ICommand ApplyThemeCommand => new RelayCommand(ApplyTheme);

    private void LoadThemes(Dictionary<string, (string Uri, string Key)> themeData)
    {
        foreach (var kvp in themeData)
        {
            var resourceDict = new ResourceDictionary
            {
                Source = new Uri(kvp.Value.Uri, UriKind.RelativeOrAbsolute)
            };

            Brush primaryBrush = Brushes.Black;
            if (resourceDict.Contains("PrimaryBackgroundBrush") &&
               resourceDict["PrimaryBackgroundBrush"] is SolidColorBrush primaryBackgroundBrush)
            {
                primaryBrush = primaryBackgroundBrush;
            }

            Brush accentBrush = Brushes.White;
            if (resourceDict.Contains("AccentBrush") &&
       resourceDict["AccentBrush"] is SolidColorBrush accent)
            {
                accentBrush = accent;
            }

            var item = new ThemeItem
            {
                Name = GetLocalizedThemeName(kvp.Value.Key),
                ResourceUri = kvp.Value.Uri,
                PrimaryColor = primaryBrush,
                AccentColor = accentBrush,
                IsSelected = false,
                LocalizationKey = kvp.Value.Key
            };

            Themes.Add(item);
        }

        // 默认选择第一个主题
        if (Themes.Count > 0)
        {
            Themes[0].IsSelected = true;
        }
    }

    private string GetLocalizedThemeName(string key)
    {
        return key switch
        {
            "Theme_Dark" => Resources.Instance.Theme_Dark,
            "Theme_Blue" => Resources.Instance.Theme_Blue,
            "Theme_Light" => Resources.Instance.Theme_Light,
            _ => key
        };
    }

    private void OnLanguageChanged(object? sender, EventArgs e)
    {
        // 更新所有主题名称
        foreach (var theme in Themes)
        {
            if (!string.IsNullOrEmpty(theme.LocalizationKey))
            {
                theme.Name = GetLocalizedThemeName(theme.LocalizationKey);
            }
        }
    }

    public void ApplyTheme(object? uri)
    {
        if (uri is not string resourceUri)
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
