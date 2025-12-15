using RedFocus.Services;
using System.Windows;

namespace RedFocus;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var settingsService = SettingsService.Instance;
        try
        {
            LanguageService.Instance.SwitchLanguage(settingsService.CurrentLanguage);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"加载语言设置失败: {ex.Message}");
        }
        ApplyTheme(settingsService.CurrentTheme);
        System.Diagnostics.Debug.WriteLine($"设置文件位置: {settingsService.GetSettingsFilePath()}");
    }

    /// <summary>
    /// 应用主题
    /// </summary>
    private void ApplyTheme(string themeUri)
    {
        try
        {
            var existingTheme = Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source?.OriginalString?.Contains("Theme.xaml") == true);
            if (existingTheme != null)
            {
                Current.Resources.MergedDictionaries.Remove(existingTheme);
            }
            var newTheme = new ResourceDictionary
            {
                Source = new Uri(themeUri, UriKind.Relative)
            };
            Current.Resources.MergedDictionaries.Add(newTheme);
            System.Diagnostics.Debug.WriteLine($"已加载主题: {themeUri}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"应用主题失败: {ex.Message}");
        }
    }
}
