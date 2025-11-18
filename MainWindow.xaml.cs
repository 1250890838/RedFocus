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

namespace RedFocus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
}