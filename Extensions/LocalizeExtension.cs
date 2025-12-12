using RedFocus.Localization;
using RedFocus.Properties;
using System.Windows.Data;
using System.Windows.Markup;

namespace RedFocus.Extensions;


[MarkupExtensionReturnType(typeof(string))]
public class LocalizeExtension : MarkupExtension
{
    public string Key { get; set; }

    public LocalizeExtension(string key)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var binding = new Binding($"[{Key}]")
        {
            Source = TranslationSource.Instance,
            Mode = BindingMode.OneWay
        };
        return binding.ProvideValue(serviceProvider);
    }
}