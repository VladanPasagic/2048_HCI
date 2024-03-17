using System.Windows.Media;

namespace _2048.WPF.ViewModels;

public class SingleTileControlViewModel : ViewModelBase
{
    private Brush _color = new SolidColorBrush(Colors.White);
    public Brush Color
    {
        get => _color;
        set
        {
            _color = value;
            OnPropertyChanged(nameof(Color));
        }
    }

    private string _value = string.Empty;

    public string Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged(nameof(Value));
        }
    }
}
