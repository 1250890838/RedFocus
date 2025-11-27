using RedFocus.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace RedFocus.Converters
{
    internal class TimerStateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is TimerState state)
            {
                return state switch
                {
                    TimerState.Focus => "FOCUS",
                    TimerState.LongBreak => "LONG BREAK",
                    TimerState.ShortBreak => "SHORT BREAK",
                    _ => $"UNKNOWN ({state})",
                };
            }
            return "Start";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
