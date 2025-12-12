using System;
using System.Collections.Generic;
using System.Text;
using global::RedFocus.Properties;
using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace RedFocus.Localization;
public class TranslationSource : INotifyPropertyChanged
{
    public static TranslationSource Instance { get; } = new TranslationSource();

    private TranslationSource() { }
    public string this[string key]
    {
        get
        {
            return Resources.ResourceManager.GetString(key, Resources.Culture);
        }
    }

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
