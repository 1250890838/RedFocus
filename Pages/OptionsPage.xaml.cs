using RedFocus.ViewModel;
using System.Windows.Controls;

namespace RedFocus.Pages
{
    public partial class OptionsPage : UserControl
    {
        public OptionsPage()
        {
            InitializeComponent();
            // ´Ó DI ÈÝÆ÷»ñÈ¡ ViewModel
            DataContext = App.GetService<OptionsViewModel>();
        }
    }
}



