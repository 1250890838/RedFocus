using RedFocus.Properties;
using RedFocus.ViewModel;
using System.Globalization;
using System.Windows.Data;

namespace RedFocus.Converters;

internal class TimerStateToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TimerState state)
        {
            return state switch
            {
                TimerState.Focus => Resources.Instance.TimerState_Focus,
                TimerState.LongBreak => Resources.Instance.TimerState_LongBreak,
                TimerState.ShortBreak => Resources.Instance.TimerState_ShortBreak,
                _ => $"UNKNOWN ({state})",
            };
        }
        return Resources.Instance.TimerState_Start;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
