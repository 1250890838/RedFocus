using RedFocus.ViewModel;
using System.Windows.Controls;

namespace RedFocus.Pages;
public partial class ThemesPage : UserControl
{
    public ThemesPage()
    {
        InitializeComponent();
        this.DataContext = new ThemeSelectorViewModel();
    }
}
