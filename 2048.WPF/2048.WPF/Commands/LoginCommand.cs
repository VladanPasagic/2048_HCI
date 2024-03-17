using _2048.Domain.Models;
using _2048.WPF.Stores;
using _2048.WPF.Utilities;
using _2048.WPF.ViewModels;

namespace _2048.WPF.Commands;
public class LoginCommand : CommandBase
{
    private readonly LoginViewModel _loginViewModel;

    public LoginCommand(LoginViewModel loginViewModel)
    {
        _loginViewModel = loginViewModel;
    }

    public override void Execute(object? parameter)
    {
        LoginDetailsFormViewModel viewModel = _loginViewModel.LoginDetailsFormViewModel;

        if (string.IsNullOrEmpty(viewModel.Username))
        {
            _loginViewModel.ErrorMessage = "Username field cannot be blank";
            _ = Methods.WaitAndRemoveErrorMessage(_loginViewModel);
            return;
        }
        if (string.IsNullOrEmpty(viewModel.Password))
        {
            _loginViewModel.ErrorMessage = "Password field cannot be blank";
            _ = Methods.WaitAndRemoveErrorMessage(_loginViewModel);
            return;
        }
        User user = new(Guid.NewGuid(), viewModel.Username, viewModel.Password);
        try
        {
            var result = UserStore.Instance!.Login(user);
            if (!result.Item1)
            {
                _loginViewModel.ErrorMessage = result.Item2;
                _ = Methods.WaitAndRemoveErrorMessage(_loginViewModel);
                return;
            }
            UserStore.Instance!.ActiveUser = new User(user.Id, viewModel.Username);
        }
        catch
        {
            _loginViewModel.ErrorMessage = "Something unexpected happened. Please try again later.";
            _ = Methods.WaitAndRemoveErrorMessage(_loginViewModel);
            return;
        }
        NavigationStore.Instance!.CurrentViewModel = new _2048ViewModel();
    }
}
