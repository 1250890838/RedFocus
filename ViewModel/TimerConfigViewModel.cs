using RedFocus.Model;
using System.Windows.Input;

namespace RedFocus.ViewModel;
internal class TimerConfigViewModel : ViewModelBase
{
    private TimerConfig _timerConfig = new()
    {
        FocusTime = TimeSpan.FromMinutes(1),
        ShortBreakTime = TimeSpan.FromMinutes(5),
        LongBreakTime = TimeSpan.FromMinutes(15),
        Rounds = 4
    };

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
            }
        }
    }
    public ICommand ResetCommand { get; }
    #endregion
    public TimerConfigViewModel()
    {
        ResetCommand = new RelayCommand(_ => ResetToDefault());
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
    #endregion

    #region 私有成员
    private void ResetToDefault()
    {
        Load(new TimerConfig
        {
            FocusTime = TimeSpan.FromMinutes(25),
            ShortBreakTime = TimeSpan.FromMinutes(5),
            LongBreakTime = TimeSpan.FromMinutes(15),
            Rounds = 4
        });
    }
    #endregion
}
