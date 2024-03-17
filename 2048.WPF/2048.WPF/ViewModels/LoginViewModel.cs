using _2048.WPF.Commands;
using _2048.WPF.Stores;
using System.Windows.Input;

namespace _2048.WPF.ViewModels;

public class LoginViewModel : ViewModelBase, IErrorViewModel
{
    private bool _isSubmitting;

    public bool IsSubmitting
    {
        get => _isSubmitting;
        set
        {
            _isSubmitting = value;
            OnPropertyChanged(nameof(IsSubmitting));
        }
    }

    private string? _errorMessage;

    public string? ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(HasErrorMessage));
        }
    }

    public bool HasErrorMessage => !string.IsNullOrEmpty(_errorMessage);

    public LoginDetailsFormViewModel LoginDetailsFormViewModel { get; }

    public LoginViewModel()
    {
        LoginCommand = new LoginCommand(this);
        GoToRegisterCommand = new GoToRegisterCommand();
        LoginDetailsFormViewModel = new();
    }

    public ICommand LoginCommand { get; }

    public ICommand GoToRegisterCommand { get; }
}
