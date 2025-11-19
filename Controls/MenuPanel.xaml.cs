using System.Windows;
using System.Windows.Controls;
using RedFocus.Pages;

namespace RedFocus.Controls
{
    public partial class MenuPanel : UserControl
    {
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(UserControl), typeof(MenuPanel),
                new PropertyMetadata(null));

        public UserControl? CurrentPage
        {
            get => (UserControl?)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        private readonly TimerConfigurationPage _timerConfigurationPage;
        private readonly OptionsPage _optionsPage;
        private readonly ThemesPage _themesPage;
        private readonly AboutPage _aboutPage;

        public MenuPanel()
        {
            InitializeComponent();

            _timerConfigurationPage = new TimerConfigurationPage();
            _optionsPage = new OptionsPage();
            _themesPage = new ThemesPage();
            _aboutPage = new AboutPage();

            CurrentPage = _timerConfigurationPage;
            UpdateButtonStates();
        }

        private void TimerConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = _timerConfigurationPage;
            UpdateButtonStates();
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = _optionsPage;
            UpdateButtonStates();
        }

        private void ThemesButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = _themesPage;
            UpdateButtonStates();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = _aboutPage;
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            TimerConfigurationButton.Tag = ReferenceEquals(CurrentPage, _timerConfigurationPage) ? "Selected" : null;
            OptionsButton.Tag = ReferenceEquals(CurrentPage, _optionsPage) ? "Selected" : null;
            ThemesButton.Tag = ReferenceEquals(CurrentPage, _themesPage) ? "Selected" : null;
            AboutButton.Tag = ReferenceEquals(CurrentPage, _aboutPage) ? "Selected" : null;
        }
    }
}
