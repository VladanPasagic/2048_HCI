using System.Windows.Controls;

namespace _2048.WPF.Views;

/// <summary>
/// Interaction logic for _2048View.xaml
/// </summary>
public partial class _2048View : UserControl
{
    public _2048View()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        Focus();
    }
}
