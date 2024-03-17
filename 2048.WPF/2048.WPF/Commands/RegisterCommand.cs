using _2048.Domain.Models;
using _2048.WPF.Stores;
using _2048.WPF.ViewModels;

namespace _2048.WPF.Commands;

public class RegisterCommand : AsyncCommandBase
{
    private readonly RegisterViewModel _registerViewModel;

    public RegisterCommand(RegisterViewModel registerViewModel)
    {
        _registerViewModel = registerViewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        RegisterDetailsFormViewModel viewModel = _registerViewModel.RegisterDetailsFormViewModel;
        if (string.IsNullOrEmpty(viewModel.Username))
        {
            _registerViewModel.ErrorMessage = "Username field cannot be blank";
            return;
        }
        if (string.IsNullOrEmpty(viewModel.Password) || string.IsNullOrEmpty(viewModel.Password2))
        {
            _registerViewModel.ErrorMessage = "Password field cannot be blank";
            return;
        }
        if (!viewModel.Password.Equals(viewModel.Password2))
        {
            _registerViewModel.ErrorMessage = "Passwords must be matching in order to create an account.";
            return;
        }

        User user = new(Guid.NewGuid(), viewModel.Username, BCrypt.Net.BCrypt.HashPassword(viewModel.Password));

        try
        {
            var result = await UserStore.Instance!.Register(user);
            if (!result.Item1)
            {
                _registerViewModel.ErrorMessage = result.Item2;
                return;
            }
        }
        catch
        {
            _registerViewModel.ErrorMessage = "Something unexpected happened. Please try again later.";
            return;
        }
        finally
        {
            NavigationStore.Instance!.CurrentViewModel = new LoginViewModel();
        }
    }
}
