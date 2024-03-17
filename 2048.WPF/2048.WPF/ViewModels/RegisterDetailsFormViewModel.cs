namespace _2048.WPF.ViewModels;

public class RegisterDetailsFormViewModel : ViewModelBase
{
    private string? _username;

    public string? Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }
    private string? _password;

    public string? Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    private string? _password2;

    public string? Password2
    {
        get => _password2;
        set
        {
            _password2 = value;
            OnPropertyChanged(nameof(Password2));
        }
    }
}