using _2048.WPF.ViewModels;

namespace _2048.WPF.Commands;

public class ViewLeaderboardCommand : CommandBase
{
    private readonly IModalViewModel _modalViewModel;

    public ViewLeaderboardCommand(IModalViewModel modalViewModel)
    {
        _modalViewModel = modalViewModel;
    }

    public override void Execute(object? parameter)
    {
        _modalViewModel.IsOpen = true;
    }
}
