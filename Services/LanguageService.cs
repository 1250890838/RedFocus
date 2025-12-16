using RedFocus.Localization;
using RedFocus.Properties;
using System.ComponentModel;
using System.Globalization;

namespace RedFocus.Services;

/// <summary>
/// 语言管理服务 - 提供多语言支持
/// </summary>
public class LanguageService(ISettingsService settingsService) : ILanguageService, INotifyPropertyChanged
{
    private CultureInfo _currentCulture = null!;
    private readonly ISettingsService _settingsService = settingsService;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler? LanguageChanged;

    /// <summary>
    /// 当前语言文化
    /// </summary>
    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        set
        {
            if (_currentCulture?.Name != value.Name)
            {
                _currentCulture = value;
                TranslationSource.Instance.CurrentCulture = value;
                OnPropertyChanged(nameof(CurrentCulture));
                OnLanguageChanged();
                _settingsService.CurrentLanguage = value.Name;
            }
        }
    }

    /// <summary>
    /// 切换到英文
    /// </summary>
    public void SwitchToEnglish()
    {
        CurrentCulture = new CultureInfo("en-US");
    }

    /// <summary>
    /// 切换到中文
    /// </summary>
    public void SwitchToChinese()
    {
        CurrentCulture = new CultureInfo("zh-CN");
    }

    /// <summary>
    /// 根据语言代码切换语言
    /// </summary>
    public void SwitchLanguage(string cultureCode)
    {
        CurrentCulture = new CultureInfo(cultureCode);
    }

    /// <summary>
    /// 获取支持的语言列表
    /// </summary>
    public IReadOnlyList<LanguageInfo> SupportedLanguages =>
    [
        new LanguageInfo("en-US", "English"),
        new LanguageInfo("zh-CN", "中文")
    ];

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnLanguageChanged()
    {
        LanguageChanged?.Invoke(this, EventArgs.Empty);
    }
}

/// <summary>
/// 语言信息
/// </summary>
public class LanguageInfo(string code, string displayName)
{
    public string Code { get; } = code;
    public string DisplayName { get; } = displayName;

    public override string ToString() => DisplayName;
}
