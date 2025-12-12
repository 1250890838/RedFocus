using RedFocus.Services;
using RedFocus.ViewModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace RedFocus;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool _isMenuOpen = false;

    public MainWindow()
    {
        InitializeComponent();
        TimerConfigViewModel timerConfigViewModel = new ViewModel.TimerConfigViewModel();

        TimerService timerService = new TimerService();
        TimerViewModel timerViewModel = new(timerConfigViewModel, timerService);
        this.DataContext = timerViewModel;
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

    private void MenuButton_Click(object sender, RoutedEventArgs e)
    {
        if (_isMenuOpen)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }
    }

    private void ShowMenu()
    {
        _isMenuOpen = true;
        MenuContainer.Visibility = Visibility.Visible;
        var storyboard = (Storyboard)FindResource("MenuShowAnimation");
        storyboard.Completed += (s, e) => { MenuButtonText.Text = "\u003C"; };
        storyboard.Begin();
    }

    private void HideMenu()
    {
        _isMenuOpen = false;
        var storyboard = (Storyboard)FindResource("MenuHideAnimation");
        storyboard.Completed += (s, e) =>
        {
            MenuContainer.Visibility = Visibility.Collapsed;
            MenuButtonText.Text = "\u2630";
        };
        storyboard.Begin();
    }

    private void MenuOverlay_Click(object sender, MouseButtonEventArgs e)
    {
        HideMenu();
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        object context = this.DataContext;
        if (context is ViewModel.TimerViewModel timerViewModel)
        {
            timerViewModel.Reset();
        }
    }
}
