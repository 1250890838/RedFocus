using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RedFocus.ViewModel;

namespace RedFocus.Model;
internal class ThemeItem : ViewModelBase
{
    private Brush _accentColor;
    private Brush _primaryColor;
    private string _name;
    private bool _isSelected;
    private string _resourceUri;

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
}
