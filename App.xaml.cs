using RedFocus.Services;
using System.Windows;

namespace RedFocus;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        ServiceLocator.ConfigureServices();
    }

    /// <summary>
    /// 获取服务（供 XAML 或其他地方使用）
    /// </summary>
    public static T GetService<T>() where T : class
    {
        return ServiceLocator.GetService<T>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var settingsService = GetService<ISettingsService>();
        var languageService = GetService<ILanguageService>();

        try
        {
            languageService.SwitchLanguage(settingsService.CurrentLanguage);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"加载语言设置失败: {ex.Message}");
        }

        ApplyTheme(settingsService.CurrentTheme);
        System.Diagnostics.Debug.WriteLine($"设置文件位置: {settingsService.GetSettingsFilePath()}");

        var mainWindow = GetService<MainWindow>();
        mainWindow.Show();
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
