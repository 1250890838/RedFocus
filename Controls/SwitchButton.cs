using System.Windows;
using System.Windows.Controls;

namespace RedFocus.Controls;

public class SwitchButton : Button
{
    static SwitchButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchButton),
            new FrameworkPropertyMetadata(typeof(SwitchButton)));
    }

    public SwitchButton()
    {
    }

    #region 依赖属性
    public static readonly DependencyProperty IsActiveProperty =
        DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(SwitchButton),
            new PropertyMetadata(false, OnIsActiveChanged));
    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }
    private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is SwitchButton button)
        {
            bool newValue = (bool)e.NewValue;
            if (newValue)
            {
                button.UnicodeChar = "\u23F8";
            }
            else
            {
                button.UnicodeChar = "\u25B6";
            }
        }
    }

    public static readonly DependencyProperty UnicodeCharProperty =
        DependencyProperty.Register(nameof(UnicodeChar), typeof(string), typeof(SwitchButton),
            new PropertyMetadata("\u25B6"));
    public string UnicodeChar
    {
        get => (string)GetValue(UnicodeCharProperty);
        set => SetValue(UnicodeCharProperty, value);
    }

    #endregion
}
