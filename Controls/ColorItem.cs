using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RedFocus.Controls;
public class ColorItem : Control
{
    static ColorItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorItem), new FrameworkPropertyMetadata(typeof(ColorItem)));
    }

    #region 依赖属性
    static readonly DependencyProperty AccentColorProperty =
        DependencyProperty.Register(nameof(AccentColor), typeof(Color), typeof(ColorItem),
            new PropertyMetadata(Colors.Transparent));
    public Color AccentColor
    {
        get => (Color)GetValue(AccentColorProperty);
        set => SetValue(AccentColorProperty, value);
    }

    static readonly DependencyProperty PrimaryColorProperty =
        DependencyProperty.Register(nameof(PrimaryColor), typeof(Color), typeof(ColorItem),
            new PropertyMetadata(Colors.Transparent));
    public Color PrimaryColor
    {
        get => (Color)GetValue(PrimaryColorProperty);
        set => SetValue(PrimaryColorProperty, value);
    }

    static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(ColorItem),
            new PropertyMetadata(false));
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(ColorItem),
            new PropertyMetadata(false));
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    #endregion
}
