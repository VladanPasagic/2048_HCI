namespace _2048.WPF.ViewModels;

public class LoginDetailsFormViewModel : ViewModelBase
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
}
