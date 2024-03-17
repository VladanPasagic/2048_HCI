using _2048.WPF.Commands;
using _2048.WPF.Stores;
using System.Windows.Input;

namespace _2048.WPF.ViewModels;

public class RegisterViewModel : ViewModelBase, IErrorViewModel
{
    public ICommand GoToLoginCommand { get; }

    public ICommand RegisterCommand { get; }

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

    public RegisterDetailsFormViewModel RegisterDetailsFormViewModel { get; }

    public RegisterViewModel()
    {
        GoToLoginCommand = new GoToLoginCommand();
        RegisterCommand = new RegisterCommand(this);
        RegisterDetailsFormViewModel = new();
    }

}
