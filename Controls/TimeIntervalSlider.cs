using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RedFocus.Controls;
public class TimeIntervalSlider : Control
{
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
            new PropertyMetadata(TimeSpan.Zero));
    public TimeSpan TimeInterval
    {
        get => (TimeSpan)GetValue(TimeIntervalProperty);
        set => SetValue(TimeIntervalProperty, value);
    }

    public static readonly DependencyProperty MaxTimeIntervalProperty = 
        DependencyProperty.Register(nameof(MaxTimeInterval), typeof(TimeSpan), typeof(TimeIntervalSlider),
            new PropertyMetadata(TimeSpan.FromHours(1) + TimeSpan.FromMinutes(30)));
    public TimeSpan MaxTimeInterval
    {
        get => (TimeSpan)GetValue(MaxTimeIntervalProperty);
        set => SetValue(MaxTimeIntervalProperty, value);
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
}
