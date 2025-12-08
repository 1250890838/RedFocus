using Microsoft.Toolkit.Uwp.Notifications;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using System.Windows.Threading;

namespace RedFocus.ViewModel;
public enum TimerState
{
    Focus,
    ShortBreak,
    LongBreak
}

internal class TimerViewModel : ViewModelBase
{
    private DispatcherTimer _timer;
    private TimerState _currentState;
    private int _currentRound = 1;
    private double _remainingMinutes;

    [SetsRequiredMembers]
    public TimerViewModel(TimerConfigViewModel timerConfig)
    {
        TimerConfig = timerConfig;
        TimerConfig.PropertyChanged += (_, args) => OnTimeConfigChanged(args);
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(50)
        };
        _timer.Tick += Timer_Tick;
        ToggleCommand = new RelayCommand(_ =>
        {
            if (_timer.IsEnabled)
                Pause();
            else
                Start();
        });
        TimerState = TimerState.Focus;
        _remainingMinutes = TimerState switch
        {
            TimerState.Focus => TimerConfig.FocusTime.TotalMinutes,
            TimerState.ShortBreak => TimerConfig.ShortBreakTime.TotalMinutes,
            TimerState.LongBreak => TimerConfig.LongBreakTime.TotalMinutes,
            _ => 0.0
        };
    }

    #region 属性
    required public TimerConfigViewModel TimerConfig { get; init; }
    public TimerState TimerState
    {
        get => _currentState;
        private set
        {
            _currentState = value;
            OnPropertyChanged();
        }
    }
    public double TimerTotalMinutes
    {
        get
        {
            return TimerState switch
            {
                TimerState.Focus => TimerConfig.FocusTime.TotalMinutes,
                TimerState.ShortBreak => TimerConfig.ShortBreakTime.TotalMinutes,
                TimerState.LongBreak => TimerConfig.LongBreakTime.TotalMinutes,
                _ => 0.0
            };
        }
    }
    public double TimerRemainingMinutes
    {
        get => _remainingMinutes;
        private set
        {
            _remainingMinutes = value;
            OnPropertyChanged();
        }
    }
    public int CurrentRound
    {
        get => _currentRound;
        set
        {
            _currentRound = value;
            OnPropertyChanged();
        }
    }

    public bool IsRunning => _timer.IsEnabled;
    public ICommand ToggleCommand { get; }
    #endregion

    #region 公有成员
    public void ProcessRoundChanged()
    {
        string title = TimerState switch
        {
            TimerState.Focus => "专注时间到！",
            TimerState.ShortBreak => "短休息时间到！",
            TimerState.LongBreak => "长休息时间到！",
            _ => "时间到！"
        };
        TimerRemainingMinutes = 0;
        Pause();
        // 切换状态逻辑
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
            TimerState.Focus => "开始新的专注时间，继续加油！",
            TimerState.ShortBreak => "休息一下，放松片刻！",
            TimerState.LongBreak => "享受一个长休息吧！",
            _ => "新的时间段开始了！"
        };
        OnPropertyChanged(nameof(TimerTotalMinutes));
        TimerRemainingMinutes = TimerTotalMinutes;
        Start();
        ShowWindowsNotification(title, content);
    }
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
                OnPropertyChanged(nameof(TimerTotalMinutes));
                Pause();
                break;
            case nameof(TimerConfig.FocusTime):
                TimerRemainingMinutes = TimerConfig.FocusTime.TotalMinutes;
                OnPropertyChanged(nameof(TimerTotalMinutes));
                break;
            case nameof(TimerConfig.ShortBreakTime) when TimerState == TimerState.ShortBreak:
                TimerRemainingMinutes = TimerConfig.ShortBreakTime.TotalMinutes;
                OnPropertyChanged(nameof(TimerTotalMinutes));
                Pause();
                break;
            case nameof(TimerConfig.ShortBreakTime):
                TimerRemainingMinutes = TimerConfig.ShortBreakTime.TotalMinutes;
                OnPropertyChanged(nameof(TimerTotalMinutes));
                break;
            case nameof(TimerConfig.LongBreakTime) when TimerState == TimerState.LongBreak:
                TimerRemainingMinutes = TimerConfig.LongBreakTime.TotalMinutes;
                OnPropertyChanged(nameof(TimerTotalMinutes));
                Pause();
                break;
            case nameof(TimerConfig.LongBreakTime):
                TimerRemainingMinutes = TimerConfig.LongBreakTime.TotalMinutes;
                OnPropertyChanged(nameof(TimerTotalMinutes));
                break;
        }
    }
    private void Timer_Tick(object? sender, EventArgs e)
    {
        var elapsed = _timer!.Interval;
        TimerRemainingMinutes -= elapsed.TotalMinutes;
        if (TimerRemainingMinutes <= 0)
        {
            ProcessRoundChanged();
        }
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
        _timer.Start();
        OnPropertyChanged(nameof(IsRunning));
    }
    private void Pause()
    {
        _timer.Stop();
        OnPropertyChanged(nameof(IsRunning));
    }
    #endregion
}
