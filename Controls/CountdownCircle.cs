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
    private Path? _progressPath;

    static CountdownCircle()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CountdownCircle),
            new FrameworkPropertyMetadata(typeof(CountdownCircle)));
    }

    public CountdownCircle()
    {
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _progressPath = GetTemplateChild("PART_ProgressPath") as Path;
        UpdateProgressPath();
    }

    #region 依赖属性

    /// <summary>
    /// 总时长（分钟）
    /// </summary>
    public static readonly DependencyProperty TotalMinutesProperty =
        DependencyProperty.Register(nameof(TotalMinutes), typeof(double), typeof(CountdownCircle),
            new PropertyMetadata(10.0, OnTotalMinutesChanged));
    public double TotalMinutes
    {
        get => (double)GetValue(TotalMinutesProperty);
        set => SetValue(TotalMinutesProperty, value);
    }
    private static void OnTotalMinutesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CountdownCircle control)
        {
            control.UpdateProgress();
            control.UpdateDisplayText();
        }
    }

    /// <summary>
    /// 当前剩余分钟数
    /// </summary>
    public static readonly DependencyProperty RemainingMinutesProperty =
        DependencyProperty.Register(nameof(RemainingMinutes), typeof(double), typeof(CountdownCircle),
            new PropertyMetadata(10.0, OnRemainingMinutesChanged));

    public double RemainingMinutes
    {
        get => (double)GetValue(RemainingMinutesProperty);
        set => SetValue(RemainingMinutesProperty, value);
    }

    private static void OnRemainingMinutesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CountdownCircle control)
        {
            control.UpdateProgress();
            control.UpdateDisplayText();
        }
    }

    /// <summary>
    /// 进度百分比 (0-100)
    /// </summary>
    public static readonly DependencyProperty ProgressProperty =
        DependencyProperty.Register(nameof(Progress), typeof(double), typeof(CountdownCircle),
            new PropertyMetadata(100.0));

    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        private set => SetValue(ProgressProperty, value);
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
    #endregion

    #region 私有方法
    private void UpdateProgress()
    {
        if (TotalMinutes > 0)
        {
            Progress = (RemainingMinutes / TotalMinutes) * 100;
        }
        else
        {
            Progress = 0;
        }
        UpdateProgressPath();
    }

    private void UpdateDisplayText()
    {
        var timeSpan = TimeSpan.FromMinutes(RemainingMinutes);
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
