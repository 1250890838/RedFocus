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
}
