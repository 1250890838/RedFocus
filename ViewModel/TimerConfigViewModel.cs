using RedFocus.Model;
using RedFocus.Services;
using System.Windows.Input;

namespace RedFocus.ViewModel;
internal class TimerConfigViewModel : ViewModelBase
{
    private TimerConfig _timerConfig = null!;

    #region 属性
    public TimeSpan FocusTime
    {
        get => _timerConfig.FocusTime;
        set
        {
            var v = value <= TimeSpan.Zero ? TimeSpan.FromMinutes(1) : value;
            if (_timerConfig.FocusTime != v)
            {
                _timerConfig.FocusTime = v;
                OnPropertyChanged();
                SettingsService.Instance.FocusDuration = (int)v.TotalMinutes;
            }
        }
    }
    public TimeSpan ShortBreakTime
    {
        get => _timerConfig.ShortBreakTime;
        set
        {
            var v = value <= TimeSpan.Zero ? TimeSpan.FromMinutes(1) : value;
            if (_timerConfig.ShortBreakTime != v)
            {
                _timerConfig.ShortBreakTime = v;
                OnPropertyChanged();
                SettingsService.Instance.ShortBreakDuration = (int)v.TotalMinutes;
            }
        }
    }
    public TimeSpan LongBreakTime
    {
        get => _timerConfig.LongBreakTime;
        set
        {
            var v = value <= TimeSpan.Zero ? TimeSpan.FromMinutes(1) : value;
            if (_timerConfig.LongBreakTime != v)
            {
                _timerConfig.LongBreakTime = v;
                OnPropertyChanged();
                SettingsService.Instance.LongBreakDuration = (int)v.TotalMinutes;
            }
        }
    }
    public int Rounds
    {
        get => _timerConfig.Rounds;
        set
        {
            var v = value < 1 ? 1 : value;
            if (_timerConfig.Rounds != v)
            {
                _timerConfig.Rounds = v;
                OnPropertyChanged();
                SettingsService.Instance.RoundsPerCycle = v;
            }
        }
    }
    public ICommand ResetCommand { get; }
    #endregion

    public TimerConfigViewModel()
    {
        ResetCommand = new RelayCommand(_ => ResetToDefault());
        LoadFromSettings();
    }

    #region 公共成员
    public void Load(TimerConfig config)
    {
        _timerConfig = config;
        OnPropertyChanged(nameof(FocusTime));
        OnPropertyChanged(nameof(ShortBreakTime));
        OnPropertyChanged(nameof(LongBreakTime));
        OnPropertyChanged(nameof(Rounds));
    }
    public TimerConfig Export() => _timerConfig;
    public void ResetToDefault()
    {
        SettingsService.Instance.ResetToDefaults();
        LoadFromSettings();
    }
    #endregion

    #region 私有成员
    private void LoadFromSettings()
    {
        var settings = SettingsService.Instance;
        Load(new TimerConfig
        {
            FocusTime = TimeSpan.FromMinutes(settings.FocusDuration),
            ShortBreakTime = TimeSpan.FromMinutes(settings.ShortBreakDuration),
            LongBreakTime = TimeSpan.FromMinutes(settings.LongBreakDuration),
            Rounds = settings.RoundsPerCycle
        });
    }
    #endregion
}
