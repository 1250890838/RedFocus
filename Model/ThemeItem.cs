using RedFocus.ViewModel;
using System.Windows.Media;

namespace RedFocus.Model;
internal class ThemeItem : ViewModelBase
{
    private Brush _accentColor = Brushes.Transparent;
    private Brush _primaryColor = Brushes.Transparent;
    private string _name = string.Empty;
    private bool _isSelected;
    private string _resourceUri = string.Empty;
    private string _localizationKey = string.Empty;

    public Brush AccentColor
    {
        get => _accentColor;
        set => SetProperty(ref _accentColor, value);
    }

    public Brush PrimaryColor
    {
        get => _primaryColor;
        set => SetProperty(ref _primaryColor, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public string ResourceUri
    {
        get => _resourceUri;
        set => SetProperty(ref _resourceUri, value);
    }

    /// <summary>
    /// 本地化资源键，用于多语言支持
    /// </summary>
    public string LocalizationKey
    {
        get => _localizationKey;
        set => SetProperty(ref _localizationKey, value);
    }
}
