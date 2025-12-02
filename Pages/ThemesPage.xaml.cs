using RedFocus.Controls;
using System.Windows;
using System.Windows.Controls;

namespace RedFocus.Pages;
public partial class ThemesPage : UserControl
{
    public ThemesPage()
    {
        InitializeComponent();
    }

    void OnColorItemSelected(object? sender,bool arg)
    {

    }

    private void OnColorItemSelected(object sender, RoutedEventArgs e)
    {
        if (!(e.OriginalSource is ColorItem colorItem))
        {
            return;
        }
        foreach (var child in ColorItemsContainer.Children)
        {
            if (child is ColorItem item)
            {
                if (item != colorItem)
                {
                    item.IsSelected = false;
                }
            }
        }

        ThemeManager.ApplyTheme(colorItem.Name switch
        {
            "LightThemeItem" => Theme.Light,
            "DarkThemeItem" => Theme.Dark,
            "BlueThemeItem" => Theme.Blue,
            _ => Theme.Dark
        });
    }
}
