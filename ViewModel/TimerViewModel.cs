using Microsoft.Toolkit.Uwp.Notifications;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using System.Windows.Threading;
using RedFocus.Services;
using RedFocus.Properties;
using RedFocus.Localization;

namespace RedFocus.ViewModel;
public enum TimerState
{
    Focus,
    ShortBreak,
    LongBreak
}

internal class TimerViewModel : ViewModelBase
{
    private TimerState _currentState;
    private int _currentRound = 1;
    private readonly TimerService _timerService;

    [SetsRequiredMembers]
    public TimerViewModel(TimerConfigViewModel timerConfig, TimerService service)
    {
        TimerConfig = timerConfig;
        _timerService = service;
        TimerConfig.PropertyChanged += (_, args) => OnTimeConfigChanged(args);
        ToggleCommand = new RelayCommand(_ =>
        {
            if (_timerService.IsRunning)
                _timerService.Pause();
            else
                _timerService.Start();
        });
        TimerState = TimerState.Focus;
        _timerService.TimerCompleted += OnTimerCompleted;
        _timerService.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(_timerService.RemainingMinutes))
            {
                OnPropertyChanged(nameof(TimerRemainingMinutes));
            }
            else if (args.PropertyName == nameof(_timerService.IsRunning))
            {
                OnPropertyChanged(nameof(IsRunning));
            }
            else if (args.PropertyName == nameof(_timerService.TotalMinutes))
            {
                OnPropertyChanged(nameof(TimerTotalMinutes));
            }
        };
        TranslationSource.Instance.PropertyChanged += (_, _) =>
        {
            OnPropertyChanged(nameof(TimerState));
        };
    }

    #region 属性
    required public TimerConfigViewModel TimerConfig { get; init; }
    public TimerState TimerState
    {
        get => _currentState;
        private set
        {
            SetProperty(ref _currentState, value);
            TimerTotalMinutes = TimerState switch
            {
                TimerState.Focus => TimerConfig.FocusTime.TotalMinutes,
                TimerState.ShortBreak => TimerConfig.ShortBreakTime.TotalMinutes,
                TimerState.LongBreak => TimerConfig.LongBreakTime.TotalMinutes,
                _ => 0.0
            };
            TimerRemainingMinutes = TimerTotalMinutes;
        }

    }
    public double TimerTotalMinutes
    {
        get => _timerService.TotalMinutes;
        private set => _timerService.TotalMinutes = value;
    }
    public double TimerRemainingMinutes
    {
        get => _timerService.RemainingMinutes;
        private set => _timerService.RemainingMinutes = value;

    }
    public int CurrentRound
    {
        get => _currentRound;
        private set => SetProperty(ref _currentRound, value);

    }
    public bool IsRunning => _timerService.IsRunning;
    public ICommand ToggleCommand { get; }
    public ICommand NextRoundCommand => new RelayCommand(_ =>
    {
        OnTimerCompleted(this, System.EventArgs.Empty);
    });
    #endregion

    #region 公有成员
    public void Reset()
    {
        TimerRemainingMinutes = TimerTotalMinutes;
        Pause();
    }
    #endregion

    #region 私有成员
    private void OnTimeConfigChanged(PropertyChangedEventArgs args)
    {
        switch (args.PropertyName)
        {
            case nameof(TimerConfig.FocusTime) when TimerState == TimerState.Focus:
                TimerRemainingMinutes = TimerConfig.FocusTime.TotalMinutes;
                Pause();
                break;
            case nameof(TimerConfig.ShortBreakTime) when TimerState == TimerState.ShortBreak:
                TimerRemainingMinutes = TimerConfig.ShortBreakTime.TotalMinutes;
                Pause();
                break;
            case nameof(TimerConfig.LongBreakTime) when TimerState == TimerState.LongBreak:
                TimerRemainingMinutes = TimerConfig.LongBreakTime.TotalMinutes;
                Pause();
                break;
        }
    }

    private void OnTimerCompleted(object? sender, System.EventArgs e)
    {
        string title = TimerState switch
        {
            TimerState.Focus => TranslationSource.Instance["TimeTo_Focus"],
            TimerState.ShortBreak => TranslationSource.Instance["TimeTo_ShortBreak"],
            TimerState.LongBreak => TranslationSource.Instance["TimeTo_LongBreak"],
            _ => "Unkown Timer State"
        };
        if (TimerState == TimerState.Focus)
        {
            if (CurrentRound == TimerConfig.Rounds)
            {
                TimerState = TimerState.LongBreak;
            }
            else
            {
                TimerState = TimerState.ShortBreak;
            }
        }
        else
        {
            if (TimerState == TimerState.LongBreak)
            {
                CurrentRound = 1;
            }
            else
            {
                CurrentRound++;
            }
            TimerState = TimerState.Focus;
        }
        string content = TimerState switch
        {
            TimerState.Focus => TranslationSource.Instance["Start_FocusTime"],
            TimerState.ShortBreak =>TranslationSource.Instance["Start_ShortBreak"],
            TimerState.LongBreak =>TranslationSource.Instance["Start_LongBreak"],
            _ => "Unkown Timer State"
        };
        Start();
        ShowWindowsNotification(title, content);
    }

    private void ShowWindowsNotification(string title, string content)
    {
        var builder = new ToastContentBuilder()
            .AddText(title)
            .AddText(content);
        builder.Show();
    }
    private void Start()
    {
        _timerService.Start();
    }
    private void Pause()
    {
        _timerService.Pause();
    }
    #endregion
}
