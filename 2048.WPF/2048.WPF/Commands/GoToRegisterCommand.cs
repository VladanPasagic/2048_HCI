using _2048.WPF.Stores;
using _2048.WPF.ViewModels;

namespace _2048.WPF.Commands;

public class GoToRegisterCommand : CommandBase
{

    public override void Execute(object? parameter)
    {
        NavigationStore.Instance!.CurrentViewModel = new RegisterViewModel();
    }
}
