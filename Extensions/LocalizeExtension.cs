using RedFocus.Properties;
using System.Windows.Data;

namespace RedFocus.Extensions;

/// <summary>
/// 本地化绑定扩展 - 用于在 XAML 中绑定本地化字符串
/// </summary>
public class LocalizeExtension : Binding
{
    public LocalizeExtension(string key) : base(key)
    {
        Source = Resources.Instance;
        Mode = BindingMode.OneWay;
    }
}
