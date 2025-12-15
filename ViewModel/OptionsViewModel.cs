using RedFocus.Services;
using System.Collections.ObjectModel;

namespace RedFocus.ViewModel;

internal class OptionsViewModel : ViewModelBase
{
    private LanguageInfo _selectedLanguage;

    public OptionsViewModel()
    {
        Languages = new ObservableCollection<LanguageInfo>(LanguageService.SupportedLanguages);

        var currentCultureCode = LanguageService.Instance.CurrentCulture.Name;
        _selectedLanguage = Languages.FirstOrDefault(l => l.Code == currentCultureCode)
            ?? Languages.First();
    }

    /// <summary>
    /// 支持的语言列表
    /// </summary>
    public ObservableCollection<LanguageInfo> Languages { get; }

    /// <summary>
    /// 当前选中的语言
    /// </summary>
    public LanguageInfo SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            if (_selectedLanguage != value && value != null)
            {
                _selectedLanguage = value;
                OnPropertyChanged();
                LanguageService.Instance.SwitchLanguage(value.Code);
            }
        }
    }
}
