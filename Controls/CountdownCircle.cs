using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RedFocus.Controls;

/// <summary>
/// 倒计时圆圈控件
/// </summary>
public class CountdownCircle : Control
{
    private DispatcherTimer? _timer;
    private Path? _progressPath;

    static CountdownCircle()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CountdownCircle),
            new FrameworkPropertyMetadata(typeof(CountdownCircle)));
    }

    public CountdownCircle()
    {
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(50)
        };
        _timer.Tick += Timer_Tick;
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _progressPath = GetTemplateChild("PART_ProgressPath") as Path;
        UpdateProgressPath();
    }

    #region 依赖属性

    /// <summary>
    /// 总时长（秒）
    /// </summary>
    public static readonly DependencyProperty TotalSecondsProperty =
        DependencyProperty.Register(nameof(TotalSeconds), typeof(double), typeof(CountdownCircle),
            new PropertyMetadata(10.0, OnTotalSecondsChanged));

    public double TotalSeconds
    {
        get => (double)GetValue(TotalSecondsProperty);
        set => SetValue(TotalSecondsProperty, value);
    }

    private static void OnTotalSecondsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CountdownCircle control)
        {
            control.Reset();
        }
    }

    /// <summary>
    /// 当前剩余秒数
    /// </summary>
    public static readonly DependencyProperty RemainingSecondsProperty =
        DependencyProperty.Register(nameof(RemainingSeconds), typeof(double), typeof(CountdownCircle),
            new PropertyMetadata(60.0, OnRemainingSecondsChanged));

    public double RemainingSeconds
    {
        get => (double)GetValue(RemainingSecondsProperty);
        private set => SetValue(RemainingSecondsProperty, value);
    }

    private static void OnRemainingSecondsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CountdownCircle control)
        {
            control.UpdateProgress();
        }
    }

    /// <summary>
    /// 进度百分比 (0-100)
    /// </summary>
    public static readonly DependencyProperty ProgressProperty =
        DependencyProperty.Register(nameof(Progress), typeof(double), typeof(CountdownCircle),
            new PropertyMetadata(100.0, OnProgressChanged));

    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        private set => SetValue(ProgressProperty, value);
    }

    private static void OnProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CountdownCircle control)
        {
            control.UpdateProgressPath();
        }
    }

    /// <summary>
    /// 圆圈颜色
    /// </summary>
    public static readonly DependencyProperty CircleColorProperty =
        DependencyProperty.Register(nameof(CircleColor), typeof(Brush), typeof(CountdownCircle),
            new PropertyMetadata(new SolidColorBrush(Colors.DodgerBlue)));

    public Brush CircleColor
    {
        get => (Brush)GetValue(CircleColorProperty);
        set => SetValue(CircleColorProperty, value);
    }

    /// <summary>
    /// 圆圈粗细
    /// </summary>
    public static readonly DependencyProperty CircleThicknessProperty =
        DependencyProperty.Register(nameof(CircleThickness), typeof(double), typeof(CountdownCircle),
            new PropertyMetadata(8.0));

    public double CircleThickness
    {
        get => (double)GetValue(CircleThicknessProperty);
        set => SetValue(CircleThicknessProperty, value);
    }

    /// <summary>
    /// 背景圆圈颜色
    /// </summary>
    public static readonly DependencyProperty BackgroundCircleColorProperty =
        DependencyProperty.Register(nameof(BackgroundCircleColor), typeof(Brush), typeof(CountdownCircle),
            new PropertyMetadata(new SolidColorBrush(Color.FromArgb(50, 100, 100, 100))));

    public Brush BackgroundCircleColor
    {
        get => (Brush)GetValue(BackgroundCircleColorProperty);
        set => SetValue(BackgroundCircleColorProperty, value);
    }

    /// <summary>
    /// 是否显示文本
    /// </summary>
    public static readonly DependencyProperty ShowTextProperty =
        DependencyProperty.Register(nameof(ShowText), typeof(bool), typeof(CountdownCircle),
            new PropertyMetadata(true));

    public bool ShowText
    {
        get => (bool)GetValue(ShowTextProperty);
        set => SetValue(ShowTextProperty, value);
    }

    /// <summary>
    /// 文本颜色
    /// </summary>
    public static readonly DependencyProperty TextColorProperty =
        DependencyProperty.Register(nameof(TextColor), typeof(Brush), typeof(CountdownCircle),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

    public Brush TextColor
    {
        get => (Brush)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// 显示的文本
    /// </summary>
    public static readonly DependencyProperty DisplayTextProperty =
        DependencyProperty.Register(nameof(DisplayText), typeof(string), typeof(CountdownCircle),
            new PropertyMetadata("01:00"));

    public string DisplayText
    {
        get => (string)GetValue(DisplayTextProperty);
        private set => SetValue(DisplayTextProperty, value);
    }

    /// <summary>
    /// 是否正在运行
    /// </summary>
    public static readonly DependencyProperty IsRunningProperty =
        DependencyProperty.Register(nameof(IsRunning), typeof(bool), typeof(CountdownCircle),
            new PropertyMetadata(false));

    public bool IsRunning
    {
        get => (bool)GetValue(IsRunningProperty);
        private set => SetValue(IsRunningProperty, value);
    }

    #endregion

    #region 路由事件

    /// <summary>
    /// 倒计时完成事件
    /// </summary>
    public static readonly RoutedEvent CompletedEvent =
        EventManager.RegisterRoutedEvent(nameof(Completed), RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(CountdownCircle));

    public event RoutedEventHandler Completed
    {
        add => AddHandler(CompletedEvent, value);
        remove => RemoveHandler(CompletedEvent, value);
    }

    /// <summary>
    /// 倒计时tick事件（每次更新时触发）
    /// </summary>
    public static readonly RoutedEvent TickEvent =
        EventManager.RegisterRoutedEvent(nameof(Tick), RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(CountdownCircle));

    public event RoutedEventHandler Tick
    {
        add => AddHandler(TickEvent, value);
        remove => RemoveHandler(TickEvent, value);
    }

    #endregion

    #region 公共方法

    /// <summary>
    /// 开始倒计时
    /// </summary>
    public void Start()
    {
        if (_timer == null || IsRunning) return;
        _timer.Start();
        IsRunning = true;
    }

    /// <summary>
    /// 暂停倒计时
    /// </summary>
    public void Pause()
    {
        if (_timer == null || !IsRunning) return;
        _timer.Stop();
        IsRunning = false;
    }

    /// <summary>
    /// 重置倒计时
    /// </summary>
    public void Reset()
    {
        Pause();
        RemainingSeconds = TotalSeconds;
        UpdateDisplayText();
    }

    #endregion

    #region 私有方法

    private void Timer_Tick(object? sender, EventArgs e)
    {
        var elapsed = _timer!.Interval;
        RemainingSeconds -= elapsed.TotalSeconds;
        if (RemainingSeconds <= 0)
        {
            RemainingSeconds = 0;
            Pause();
            RaiseEvent(new RoutedEventArgs(CompletedEvent, this));
        }
        else
        {
          //  RaiseEvent(new RoutedEventArgs(TickEvent, this));
        }
        UpdateDisplayText();
    }

    private void UpdateProgress()
    {
        if (TotalSeconds > 0)
        {
            Progress = (RemainingSeconds / TotalSeconds) * 100;
        }
        else
        {
            Progress = 0;
        }
    }

    private void UpdateDisplayText()
    {
        var timeSpan = TimeSpan.FromSeconds(RemainingSeconds);
        DisplayText = $"{(int)timeSpan.TotalMinutes}:{timeSpan.Seconds:D2}";
    }

    private void UpdateProgressPath()
    {
        if (_progressPath == null) return;

        double canvasSize = 200;
        double radius = canvasSize / 2.0;
        double centerX = canvasSize / 2.0;
        double centerY = canvasSize / 2.0;

        double progressAngle = (Progress / 100.0) * 360.0;

        if (progressAngle <= 0)
        {
            _progressPath.Visibility = Visibility.Collapsed;
            return;
        }

        _progressPath.Visibility = Visibility.Visible;

        Point startPoint = new Point(centerX, centerY - radius);

        PathFigure pathFigure = new PathFigure
        {
            StartPoint = startPoint,
            IsClosed = false
        };

        if (progressAngle >= 359.9)
        {
            Point midPoint = new Point(centerX, centerY + radius);
            pathFigure.Segments.Add(new ArcSegment(
                midPoint,
                new Size(radius, radius),
                0,
                false,
                SweepDirection.Clockwise,
                true));
            pathFigure.Segments.Add(new ArcSegment(
                new Point(startPoint.X, startPoint.Y - 0.1),
                new Size(radius, radius),
                0,
                false,
                SweepDirection.Clockwise,
                true));
        }
        else
        {
            double radians = (progressAngle - 90) * Math.PI / 180.0;
            double endX = centerX + radius * Math.Cos(radians);
            double endY = centerY + radius * Math.Sin(radians);
            Point endPoint = new Point(endX, endY);

            bool isLargeArc = progressAngle > 180;
            pathFigure.Segments.Add(new ArcSegment(
                endPoint,
                new Size(radius, radius),
                0,
                isLargeArc,
                SweepDirection.Clockwise,
                true));
        }

        PathGeometry pathGeometry = new PathGeometry();
        pathGeometry.Figures.Add(pathFigure);

        _progressPath.Data = pathGeometry;
    }

    #endregion
}
