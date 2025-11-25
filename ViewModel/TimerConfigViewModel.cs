using System;
using System.Windows.Input;
using RedFocus.Model;

namespace RedFocus.ViewModel;
public class TimerConfigViewModel : ViewModelBase
{
    private TimerConfig _timerConfig = new()
    {
        FocusTime = TimeSpan.FromMinutes(25),
        ShortBreakTime = TimeSpan.FromMinutes(5),
        LongBreakTime = TimeSpan.FromMinutes(15),
        Rounds = 4
    };
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
    public TimerConfigViewModel()
    {
        ResetCommand = new RelayCommand(_ => ResetToDefault());
    }
    public void Load(TimerConfig config)
    {
        _timerConfig = config;
        OnPropertyChanged(nameof(FocusTime));
        OnPropertyChanged(nameof(ShortBreakTime));
        OnPropertyChanged(nameof(LongBreakTime));
        OnPropertyChanged(nameof(Rounds));
    }
    public TimerConfig Export() => _timerConfig;
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
    private class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute(parameter);
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }
}
