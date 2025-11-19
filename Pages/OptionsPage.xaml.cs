using System.Windows;
using System.Windows.Controls;

namespace RedFocus.Pages
{
    public partial class OptionsPage : UserControl
    {
        public OptionsPage()
        {
       InitializeComponent();
        }

private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
        ThemeManager.ApplyTheme(Theme.Dark);
        }

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme(Theme.Light);
        }

        private void BlueTheme_Click(object sender, RoutedEventArgs e)
        {
          ThemeManager.ApplyTheme(Theme.Blue);
        }
    }
}
