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
public class ThemeItem
{
    public Brush AccentColor { get; set; }
    public Brush PrimaryColor { get; set; }
    public string Name { get; set; }
    public bool IsSelected { get; set; }
    public string ResourceUri { get; set; }
}
