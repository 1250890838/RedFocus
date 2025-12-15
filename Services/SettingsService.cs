using System.IO;
using System.Text.Json;

namespace RedFocus.Services;

/// <summary>
/// 设置服务 - 负责保存和加载用户设置到 JSON 文件
/// </summary>
public sealed class SettingsService
{
    private static readonly Lazy<SettingsService> _instance = new(() => new SettingsService());
    public static SettingsService Instance = _instance.Value;
    private readonly string _settingsFilePath;
    private UserSettings _settings;

    private SettingsService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var appFolder = Path.Combine(appDataPath, "RedFocus");

        if (!Directory.Exists(appFolder))
        {
            Directory.CreateDirectory(appFolder);
        }

        _settingsFilePath = Path.Combine(appFolder, "settings.json");
        _settings = LoadSettings();
    }

    /// <summary>
    /// 当前主题资源URI
    /// </summary>
    public string CurrentTheme
    {
        get => _settings.ThemeUri;
        set
        {
            if (_settings.ThemeUri != value)
            {
                _settings.ThemeUri = value;
                SaveSettings();
            }
        }
    }

    /// <summary>
    /// 当前语言代码
    /// </summary>
    public string CurrentLanguage
    {
        get => _settings.LanguageCode;
        set
        {
            if (_settings.LanguageCode != value)
            {
                _settings.LanguageCode = value;
                SaveSettings();
            }
        }
    }

    /// <summary>
    /// 专注时长（分钟）
    /// </summary>
    public int FocusDuration
    {
        get => _settings.FocusDuration;
        set
        {
            if (_settings.FocusDuration != value)
            {
                _settings.FocusDuration = value;
                SaveSettings();
            }
        }
    }

    /// <summary>
    /// 短休息时长（分钟）
    /// </summary>
    public int ShortBreakDuration
    {
        get => _settings.ShortBreakDuration;
        set
        {
            if (_settings.ShortBreakDuration != value)
            {
                _settings.ShortBreakDuration = value;
                SaveSettings();
            }
        }
    }

    /// <summary>
    /// 长休息时长（分钟）
    /// </summary>
    public int LongBreakDuration
    {
        get => _settings.LongBreakDuration;
        set
        {
            if (_settings.LongBreakDuration != value)
            {
                _settings.LongBreakDuration = value;
                SaveSettings();
            }
        }
    }

    /// <summary>
    /// 每轮次数
    /// </summary>
    public int RoundsPerCycle
    {
        get => _settings.RoundsPerCycle;
        set
        {
            if (_settings.RoundsPerCycle != value)
            {
                _settings.RoundsPerCycle = value;
                SaveSettings();
            }
        }
    }

    /// <summary>
    /// 从 JSON 文件加载设置
    /// </summary>
    private UserSettings LoadSettings()
    {
        try
        {
            if (File.Exists(_settingsFilePath))
            {
                var json = File.ReadAllText(_settingsFilePath);
                var settings = JsonSerializer.Deserialize<UserSettings>(json);
                return settings ?? CreateDefaultSettings();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"加载设置失败: {ex.Message}");
        }

        return CreateDefaultSettings();
    }

    /// <summary>
    /// 保存设置到 JSON 文件
    /// </summary>
    private void SaveSettings()
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var json = JsonSerializer.Serialize(_settings, options);
            File.WriteAllText(_settingsFilePath, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"保存设置失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 创建默认设置
    /// </summary>
    private UserSettings CreateDefaultSettings()
    {
        return new UserSettings
        {
            ThemeUri = "/Themes/DarkTheme.xaml",
            LanguageCode = "zh-CN",
            FocusDuration = 25,
            ShortBreakDuration = 5,
            LongBreakDuration = 15,
            RoundsPerCycle = 4
        };
    }

    /// <summary>
    /// 重置为默认设置
    /// </summary>
    public void ResetToDefaults()
    {
        _settings = CreateDefaultSettings();
        SaveSettings();
    }

    /// <summary>
    /// 获取设置文件路径（用于调试）
    /// </summary>
    public string GetSettingsFilePath() => _settingsFilePath;
}

/// <summary>
/// 用户设置数据模型
/// </summary>
internal class UserSettings
{
    public string ThemeUri { get; set; } = "/Themes/DarkTheme.xaml";
    public string LanguageCode { get; set; } = "zh-CN";
    public int FocusDuration { get; set; } = 25;
    public int ShortBreakDuration { get; set; } = 5;
    public int LongBreakDuration { get; set; } = 15;
    public int RoundsPerCycle { get; set; } = 4;
}
