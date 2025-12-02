using RedFocus.Controls;
using System.Windows;
using System.Windows.Controls;
using RedFocus.ViewModel;

namespace RedFocus.Pages;
public partial class ThemesPage : UserControl
{
    public ThemesPage()
    {
        InitializeComponent();
        this.DataContext = new ThemeSelectorViewModel();
    }
    private void ThemeItemControl_Selected(object sender, RoutedEventArgs e)
    {
        if (sender is ThemeItemControl themeItem)
        {
            ThemeSelectorViewModel.ApplyTheme(themeItem.ResourceUri);
        }

        var viewModel = this.DataContext as ThemeSelectorViewModel;
        foreach (var item in viewModel.Themes)
        {
            if (item.ResourceUri != (sender as ThemeItemControl)?.ResourceUri)
                item.IsSelected = false;
        }

        e.Handled = true;
    }
}
