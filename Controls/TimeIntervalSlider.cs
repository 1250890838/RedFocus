using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RedFocus.Controls;
public class TimeIntervalSlider : Control
{
    private Slider? _slider;
    static TimeIntervalSlider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeIntervalSlider),
            new FrameworkPropertyMetadata(typeof(TimeIntervalSlider)));
    }

    #region 依赖属性
    public static readonly DependencyProperty TrackBrushProperty =
        DependencyProperty.Register(nameof(TrackBrush), typeof(Brush), typeof(TimeIntervalSlider),
            new PropertyMetadata(null));
    public Brush TrackBrush
    {
        get => (Brush)GetValue(TrackBrushProperty);
        set => SetValue(TrackBrushProperty, value);
    }

    public static readonly DependencyProperty ProgressBrushProperty =
        DependencyProperty.Register(nameof(ProgressBrush), typeof(Brush), typeof(TimeIntervalSlider),
            new PropertyMetadata(null));
    public Brush ProgressBrush
    {
        get => (Brush)GetValue(ProgressBrushProperty);
        set => SetValue(ProgressBrushProperty, value);
    }

    public static readonly DependencyProperty TimeIntervalProperty =
        DependencyProperty.Register(nameof(TimeInterval), typeof(TimeSpan), typeof(TimeIntervalSlider),
            new PropertyMetadata(TimeSpan.FromMinutes(15), null, CoerceTimeIntervalCallback));
    public TimeSpan TimeInterval
    {
        get => (TimeSpan)GetValue(TimeIntervalProperty);
        set => SetValue(TimeIntervalProperty, value);
    }

    public static object CoerceTimeIntervalCallback(DependencyObject d, object baseValue)
    {
        if (d is TimeIntervalSlider t)
        {
            if ((TimeSpan)baseValue > t.MaxTimeInterval)
            {
                return t.MaxTimeInterval;
            }
            else
            {
                return baseValue;
            }
        }
        return TimeSpan.Zero;
    }

    public static readonly DependencyProperty MaxTimeIntervalProperty =
        DependencyProperty.Register(nameof(MaxTimeInterval), typeof(TimeSpan), typeof(TimeIntervalSlider),
            new PropertyMetadata(TimeSpan.FromHours(1) + TimeSpan.FromMinutes(30), MaxTimeIntervalChangedCallback, null));
    public TimeSpan MaxTimeInterval
    {
        get => (TimeSpan)GetValue(MaxTimeIntervalProperty);
        set
        {
            SetValue(MaxTimeIntervalProperty, value);
        }
    }

    public static void MaxTimeIntervalChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.CoerceValue(TimeIntervalProperty);
    }

    public static readonly DependencyProperty TimeIntervalTextBrushProperty =
        DependencyProperty.Register(nameof(TimeIntervalTextBrush), typeof(Brush), typeof(TimeIntervalSlider),
            new PropertyMetadata(null));
    public Brush TimeIntervalTextBrush
    {
        get => (Brush)GetValue(TimeIntervalTextBrushProperty);
        set => SetValue(TimeIntervalTextBrushProperty, value);
    }

    public static readonly DependencyProperty TimeIntervalTextBackgroundBrushProperty =
        DependencyProperty.Register(nameof(TimeIntervalTextBackgroundBrush), typeof(Brush), typeof(TimeIntervalSlider),
            new PropertyMetadata(null));
    public Brush TimeIntervalTextBackgroundBrush
    {
        get => (Brush)GetValue(TimeIntervalTextBackgroundBrushProperty);
        set => SetValue(TimeIntervalTextBackgroundBrushProperty, value);
    }

    public static readonly DependencyProperty TimeIntervalTextFontSizeProperty =
        DependencyProperty.Register(nameof(TimeIntervalTextFontSize), typeof(double), typeof(TimeIntervalSlider),
            new PropertyMetadata(12.0));
    public double TimeIntervalTextFontSize
    {
        get => (double)GetValue(TimeIntervalTextFontSizeProperty);
        set => SetValue(TimeIntervalTextFontSizeProperty, value);
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(TimeIntervalSlider),
            new PropertyMetadata(string.Empty));
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty TitleFontSizeProperty =
        DependencyProperty.Register(nameof(TitleFontSize), typeof(double), typeof(TimeIntervalSlider),
            new PropertyMetadata(12.0));
    public double TitleFontSize
    {
        get => (double)GetValue(TitleFontSizeProperty);
        set => SetValue(TitleFontSizeProperty, value);
    }

    public static readonly DependencyProperty TitleBrushProperty =
        DependencyProperty.Register(nameof(TitleBrush), typeof(Brush), typeof(TimeIntervalSlider),
            new PropertyMetadata(null));
    public Brush TitleBrush
    {
        get => (Brush)GetValue(TitleBrushProperty);
        set => SetValue(TitleBrushProperty, value);
    }



    #endregion

    #region 事件
    public static readonly RoutedEvent SlideCompletedEvent =
        EventManager.RegisterRoutedEvent(nameof(SlideCompleted), RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<TimeSpan>), typeof(TimeIntervalSlider));
    public event RoutedPropertyChangedEventHandler<TimeSpan> SlideCompleted
    {
        add => AddHandler(SlideCompletedEvent, value);
        remove => RemoveHandler(SlideCompletedEvent, value);
    }

    #endregion

    #region 公共方法

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        if (_slider != null)
            _slider.ValueChanged -= OnSliderValueChangedChanged;
        _slider = GetTemplateChild("InnerSlider") as Slider;
        if (_slider != null)
            _slider.ValueChanged += OnSliderValueChangedChanged;
    }

    public void OnSliderValueChangedChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Slider? slider = sender as Slider;
        var args = new RoutedPropertyChangedEventArgs<TimeSpan>(
            TimeSpan.FromMinutes(e.OldValue),
            TimeSpan.FromMinutes(e.NewValue),
            SlideCompletedEvent);
        RaiseEvent(args);
    }
    #endregion
}
