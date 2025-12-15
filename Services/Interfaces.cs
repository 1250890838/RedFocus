using System.Globalization;

namespace RedFocus.Services;

/// <summary>
/// 设置服务接口
/// </summary>
public interface ISettingsService
{
    string CurrentTheme { get; set; }
    string CurrentLanguage { get; set; }
    int FocusDuration { get; set; }
    int ShortBreakDuration { get; set; }
    int LongBreakDuration { get; set; }
    int RoundsPerCycle { get; set; }
    void ResetToDefaults();
    string GetSettingsFilePath();
}

/// <summary>
/// 语言服务接口
/// </summary>
public interface ILanguageService
{
    CultureInfo CurrentCulture { get; set; }
    event EventHandler? LanguageChanged;
    void SwitchToEnglish();
    void SwitchToChinese();
    void SwitchLanguage(string cultureCode);
    IReadOnlyList<LanguageInfo> SupportedLanguages { get; }
}

/// <summary>
/// 主题服务接口
/// </summary>
public interface IThemeService
{
    string CurrentTheme { get; }
    void ApplyTheme(string themeUri);
    IReadOnlyList<ThemeInfo> SupportedThemes { get; }
}

/// <summary>
/// 主题信息
/// </summary>
public class ThemeInfo(string uri, string localizationKey)
{
    public string Uri { get; } = uri;
    public string LocalizationKey { get; } = localizationKey;
}
