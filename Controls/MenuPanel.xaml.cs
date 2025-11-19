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

        private readonly TimerPage _timerPage;
        private readonly TasksPage _tasksPage;
        private readonly StatisticsPage _statisticsPage;
        private readonly SettingsPage _settingsPage;

        public MenuPanel()
        {
            InitializeComponent();

            _timerPage = new TimerPage();
            _tasksPage = new TasksPage();
            _statisticsPage = new StatisticsPage();
            _settingsPage = new SettingsPage();

            CurrentPage = _timerPage;
            UpdateButtonStates();
        }

        private void TimerConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = _timerPage;
            UpdateButtonStates();
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = _tasksPage;
            UpdateButtonStates();
        }

        private void ThemesButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = _statisticsPage;
            UpdateButtonStates();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = _settingsPage;
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            TimerConfigurationButton.Tag = ReferenceEquals(CurrentPage, _timerPage) ? "Selected" : null;
            OptionsButton.Tag = ReferenceEquals(CurrentPage, _tasksPage) ? "Selected" : null;
            ThemesButton.Tag = ReferenceEquals(CurrentPage, _statisticsPage) ? "Selected" : null;
            AboutButton.Tag = ReferenceEquals(CurrentPage, _settingsPage) ? "Selected" : null;
        }
    }
}
