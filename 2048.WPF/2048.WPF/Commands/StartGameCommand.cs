using _2048.WPF.ViewModels;

namespace _2048.WPF.Commands;

public class StartGameCommand : CommandBase
{
    private readonly _2048ViewModel _viewModel;

    public StartGameCommand(_2048ViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object? parameter)
    {
        if (_viewModel.HasStarted)
            return;
        _viewModel.HasStarted = true;
        _viewModel.FIllTile();
        _viewModel.FIllTile();
    }
}
