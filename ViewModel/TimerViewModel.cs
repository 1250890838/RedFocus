using RedFocus.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RedFocus.ViewModel;
public enum TimerState
{
    Focus,
    ShortBreak,
    LongBreak
}
public class TimerViewModel : ViewModelBase
{
    public TimerConfigViewModel TimerConfig { get; init; }
    public TimerViewModel(TimerConfigViewModel timerConfig)
    {
        TimerConfig = timerConfig;
        CurrentState = TimerState.Focus;
    }

    private TimerState _currentState;
    public TimerState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan CurrentTime
    {
        get
        {
            return CurrentState switch
            {
                TimerState.Focus => TimerConfig.FocusTime,
                TimerState.ShortBreak => TimerConfig.ShortBreakTime,
                TimerState.LongBreak => TimerConfig.LongBreakTime,
                _ => TimeSpan.Zero
            };
        }
    }
}
