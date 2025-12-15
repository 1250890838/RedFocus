using RedFocus.ViewModel;
using System.Windows.Controls;

namespace RedFocus.Pages;

public partial class ThemesPage : UserControl
{
    public ThemesPage()
    {
        InitializeComponent();
        // ´Ó DI ÈÝÆ÷»ñÈ¡ ViewModel
        DataContext = App.GetService<ThemeSelectorViewModel>();
    }
}
