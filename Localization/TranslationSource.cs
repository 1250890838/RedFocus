using global::RedFocus.Properties;
using System.ComponentModel;
using System.Globalization;

namespace RedFocus.Localization;
public class TranslationSource : INotifyPropertyChanged
{
    public static TranslationSource Instance { get; } = new TranslationSource();

    private TranslationSource() { }
    public string this[string key] => Resources.ResourceManager!.GetString(key, Resources.Culture) ?? string.Empty;

    public CultureInfo CurrentCulture
    {
        get => Resources.Culture ?? CultureInfo.CurrentUICulture;
        set
        {
            if (Resources.Culture != value)
            {
                Resources.Culture = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
