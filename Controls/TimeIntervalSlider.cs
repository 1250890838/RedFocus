using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RedFocus.Controls;
public class TimeIntervalSlider : Control
{

    #region 依赖属性
    static readonly DependencyProperty TrackBrushProperty =
        DependencyProperty.Register(nameof(TrackBrush), typeof(Brush), typeof(TimeIntervalSlider),
            new PropertyMetadata(null));
    public Brush TrackBrush
    {
        get => (Brush)GetValue(TrackBrushProperty);
        set => SetValue(TrackBrushProperty, value);
    }

    static readonly DependencyProperty ProgressBrushProperty =
        DependencyProperty.Register(nameof(ProgressBrush), typeof(Brush), typeof(TimeIntervalSlider),
            new PropertyMetadata(null));
    Brush ProgressBrush
    {
        get => (Brush)GetValue(ProgressBrushProperty);
        set => SetValue(ProgressBrushProperty, value);
    }

    static readonly DependencyProperty TimeIntervalProperty =
               DependencyProperty.Register(nameof(TimeInterval), typeof(TimeSpan), typeof(TimeIntervalSlider),
            new PropertyMetadata(TimeSpan.Zero));
    TimeSpan TimeInterval
    {
        get => (TimeSpan)GetValue(TimeIntervalProperty);
        set => SetValue(TimeIntervalProperty, value);
    }

    static readonly DependencyProperty TimerIntervalTextBrushProperty =
                DependencyProperty.Register(nameof(TimerIntervalTextBrush), typeof(Brush), typeof(TimeIntervalSlider),
            new PropertyMetadata(null));
    Brush TimerIntervalTextBrush
    {
        get => (Brush)GetValue(TimerIntervalTextBrushProperty);
        set => SetValue(TimerIntervalTextBrushProperty, value);
    }

    static readonly DependencyProperty TimerIntervalTextBackgroundBrushProperty =
        DependencyProperty.Register(nameof(TimerIntervalTextBackgroundBrush), typeof(Brush), typeof(TimeIntervalSlider),
            new PropertyMetadata(null));
    Brush TimerIntervalTextBackgroundBrush
    {
        get => (Brush)GetValue(TimerIntervalTextBackgroundBrushProperty);
        set => SetValue(TimerIntervalTextBackgroundBrushProperty, value);
    }

    static readonly DependencyProperty TimerIntervalTextFontSizeProperty =
                DependencyProperty.Register(nameof(TimerIntervalTextFontSize), typeof(double), typeof(TimeIntervalSlider),
            new PropertyMetadata(12.0));
    double TimerIntervalTextFontSize
    {
        get => (double)GetValue(TimerIntervalTextFontSizeProperty);
        set => SetValue(TimerIntervalTextFontSizeProperty, value);
    }

    static readonly DependencyProperty TitleProperty = 
                       DependencyProperty.Register(nameof(Title), typeof(string), typeof(TimeIntervalSlider),
            new PropertyMetadata(string.Empty));
    string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }   

    static readonly DependencyProperty TitleFontSizeProperty =
                       DependencyProperty.Register(nameof(TitleFontSize), typeof(double), typeof(TimeIntervalSlider),
            new PropertyMetadata(12.0));
    double TitleFontSize
    {
        get => (double)GetValue(TitleFontSizeProperty);
        set => SetValue(TitleFontSizeProperty, value);
    }

    static readonly DependencyProperty TitleBrushProperty = 
               DependencyProperty.Register(nameof(TitleBrush), typeof(Brush), typeof(TimeIntervalSlider),
            new PropertyMetadata(null));
    Brush TitleBrush
    {
        get => (Brush)GetValue(TitleBrushProperty);
        set => SetValue(TitleBrushProperty, value);
    }

    #endregion
}
