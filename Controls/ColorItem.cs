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
using RedFocus.ViewModel;

namespace RedFocus.Controls;
public class ColorItem : Control
{
    static ColorItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorItem), new FrameworkPropertyMetadata(typeof(ColorItem)));
    }

    #region 依赖属性
    public static readonly DependencyProperty AccentColorProperty =
        DependencyProperty.Register(nameof(AccentColor), typeof(Brush), typeof(ColorItem),
            new PropertyMetadata(Brushes.Transparent));
    public Brush AccentColor
    {
        get => (Brush)GetValue(AccentColorProperty);
        set => SetValue(AccentColorProperty, value);
    }

    public static readonly DependencyProperty PrimaryColorProperty =
        DependencyProperty.Register(nameof(PrimaryColor), typeof(Brush), typeof(ColorItem),
            new PropertyMetadata(Brushes.Transparent));
    public Brush PrimaryColor
    {
        get => (Brush)GetValue(PrimaryColorProperty);
        set => SetValue(PrimaryColorProperty, value);
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(ColorItem),
            new PropertyMetadata(""));
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(ColorItem),
            new PropertyMetadata(false));
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    #endregion

    #region 路由事件
    public static readonly RoutedEvent SelectedEvent =
        EventManager.RegisterRoutedEvent(nameof(Selected), RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ColorItem));
    public event RoutedEventHandler Selected
    {
        add => AddHandler(SelectedEvent, value);
        remove => RemoveHandler(SelectedEvent, value);
    }
    #endregion
    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);

        if (!IsSelected)
        {
            IsSelected = true;
            e.Handled = true;
            RaiseEvent(new RoutedEventArgs(SelectedEvent, this));
        }
    }
}
