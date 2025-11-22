using System.Windows.Controls;

namespace RedFocus.Pages
{
    public partial class TimerConfigurationPage : UserControl
    {
        public TimerConfigurationPage()
        {
            InitializeComponent();
        }

        private void ShortBreakTimeSlider_SlideCompleted(object sender, System.Windows.RoutedPropertyChangedEventArgs<TimeSpan> e)
        {
#if DEBUG   
            System.Diagnostics.Debug.WriteLine($"Short Break Time Slider Slide Completed: New Value = {e.NewValue}");
#endif
        }
    }
}
