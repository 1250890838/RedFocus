using RedFocus.ViewModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Threading;

namespace RedFocus.Services;
internal interface ITimerService : INotifyPropertyChanged
{
    public void Start();
    public void Pause();
    public double TotalMinutes { get; set; }
    public double RemainingMinutes { get; set; }
    public bool IsRunning { get; }

    public event EventHandler? TimerCompleted;
}

internal class TimerService : ViewModelBase, ITimerService
{
    private readonly DispatcherTimer _timer;
    private bool _isRunning;
    private double _remainingMinutes;
    private double _totalMinutes;

    public double TotalMinutes { get => _totalMinutes; set => SetProperty(ref _totalMinutes, value); }
    public double RemainingMinutes { get => _remainingMinutes; set => SetProperty(ref _remainingMinutes, value); }
    public bool IsRunning { get => _isRunning; private set { SetProperty(ref _isRunning, value); } }

    public event EventHandler? TimerCompleted;

    [SetsRequiredMembers]
    public TimerService()
    {
        _timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(50)
        };
        _timer.Tick += OnTimerTick;
    }
    public void Start()
    {
        if (_timer.IsEnabled)
        {
            return;
        }
        _timer.Start();
        IsRunning = _timer.IsEnabled;
    }
    public void Pause()
    {
        if (_timer.IsEnabled == false)
        {
            return;
        }
        _timer.Stop();
        IsRunning = _timer.IsEnabled;
    }
    private void OnTimerTick(object? sender, EventArgs e)
    {
        var elapsed = _timer!.Interval;
        RemainingMinutes -= elapsed.TotalMinutes;
        if (RemainingMinutes <= 0)
        {
            TimerCompleted?.Invoke(this, EventArgs.Empty);
            RemainingMinutes = 0;
            Pause();
        }
    }
}
