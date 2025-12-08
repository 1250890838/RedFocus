using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RedFocus.Controls;
public class ThemeItemControl : Control
{
    static ThemeItemControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemeItemControl), new FrameworkPropertyMetadata(typeof(ThemeItemControl)));
    }

    #region 依赖属性
    public static readonly DependencyProperty AccentColorProperty =
        DependencyProperty.Register(nameof(AccentColor), typeof(Brush), typeof(ThemeItemControl),
            new PropertyMetadata(Brushes.Transparent));
    public Brush AccentColor
    {
        get => (Brush)GetValue(AccentColorProperty);
        set => SetValue(AccentColorProperty, value);
    }

    public static readonly DependencyProperty PrimaryColorProperty =
        DependencyProperty.Register(nameof(PrimaryColor), typeof(Brush), typeof(ThemeItemControl),
            new PropertyMetadata(Brushes.Transparent));
    public Brush PrimaryColor
    {
        get => (Brush)GetValue(PrimaryColorProperty);
        set => SetValue(PrimaryColorProperty, value);
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(ThemeItemControl),
            new PropertyMetadata(""));
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(ThemeItemControl),
            new PropertyMetadata(false));
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public static readonly DependencyProperty ResourceUriProperty =
        DependencyProperty.Register(nameof(ResourceUri), typeof(string), typeof(ThemeItemControl),
            new PropertyMetadata(""));
    public string ResourceUri
    {
        get => (string)GetValue(ResourceUriProperty);
        set => SetValue(ResourceUriProperty, value);
    }

    public static readonly DependencyProperty CommandProperty =
    DependencyProperty.Register(
        nameof(Command),
        typeof(ICommand),
        typeof(ThemeItemControl),
        new PropertyMetadata(null));

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion

    #region 路由事件
    public static readonly RoutedEvent SelectedEvent =
        EventManager.RegisterRoutedEvent(nameof(Selected), RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ThemeItemControl));
    public event RoutedEventHandler Selected
    {
        add => AddHandler(SelectedEvent, value);
        remove => RemoveHandler(SelectedEvent, value);
    }
    #endregion


    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);
        IsSelected = true;
        e.Handled = true;
        RaiseEvent(new RoutedEventArgs(SelectedEvent, this));
        if (Command != null && Command.CanExecute(ResourceUri))
        {
            Command.Execute(ResourceUri);
        }
    }
}
