using _2048.WPF.ViewModels;

namespace _2048.WPF.Commands;

public class CloseModalCommand : CommandBase
{
    private readonly IModalViewModel _modalViewModel;

    public CloseModalCommand(IModalViewModel modalViewModel)
    {
        _modalViewModel = modalViewModel;
    }

    public override void Execute(object? parameter)
    {
        _modalViewModel.IsOpen = false;
    }
}
