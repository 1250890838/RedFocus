using RedFocus.Services;
using System.Collections.ObjectModel;

namespace RedFocus.ViewModel;

public class OptionsViewModel : ViewModelBase
{
    private readonly ILanguageService _languageService;
    private LanguageInfo _selectedLanguage;

    public OptionsViewModel(ILanguageService languageService)
    {
        _languageService = languageService;
        Languages = new ObservableCollection<LanguageInfo>(_languageService.SupportedLanguages);

        var currentCultureCode = _languageService.CurrentCulture.Name;
        _selectedLanguage = Languages.FirstOrDefault(l => l.Code == currentCultureCode)
            ?? Languages.First();
    }

    /// <summary>
    /// 支持的语言列表
    /// </summary>
    public ObservableCollection<LanguageInfo> Languages { get; }

    /// <summary>
    /// 当前选择的语言
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
                _languageService.SwitchLanguage(value.Code);
            }
        }
    }
}
