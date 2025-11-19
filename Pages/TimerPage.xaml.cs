using System.Windows;

namespace RedFocus.Pages
{
    public partial class TimerPage
    {
        public TimerPage()
        {
            InitializeComponent();
            CountdownTimer.Completed += CountdownTimer_Completed;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            CountdownTimer.Start();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            CountdownTimer.Pause();
        }

        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            CountdownTimer.Resume();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            CountdownTimer.Reset();
        }

        private void CountdownTimer_Completed(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Countdown completed!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
