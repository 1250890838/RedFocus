using System.Windows;

namespace RedFocus.Pages;
public partial class SettingsPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private void DarkTheme_Click(object sender, RoutedEventArgs e)
    {
        ThemeManager.CurrentTheme = Theme.Dark;
    }

    private void LightTheme_Click(object sender, RoutedEventArgs e)
    {
        ThemeManager.CurrentTheme = Theme.Light;
    }

    private void BlueTheme_Click(object sender, RoutedEventArgs e)
    {
        ThemeManager.CurrentTheme = Theme.Blue;
    }
}
