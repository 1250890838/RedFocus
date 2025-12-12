using RedFocus.Localization;
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
                TimerState.Focus => TranslationSource.Instance["TimerState_Focus"],
                TimerState.LongBreak => TranslationSource.Instance["TimerState_LongBreak"],
                TimerState.ShortBreak => TranslationSource.Instance["Resources.TimerState_ShortBreak"],
                _ => $"UNKNOWN ({state})",
            };
        }
        return Resources.TimerState_Start;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
