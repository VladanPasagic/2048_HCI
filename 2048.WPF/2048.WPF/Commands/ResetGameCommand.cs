using _2048.WPF.ViewModels;
using System.Windows.Media;

namespace _2048.WPF.Commands;

public class ResetGameCommand : CommandBase
{
    private readonly _2048ViewModel _viewModel;

    public ResetGameCommand(_2048ViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object? parameter)
    {
        _viewModel.HasStarted = false;
        _viewModel.Scoreboard.CurrentScore = 0;
        _viewModel.CurrentEmptyTiles = _viewModel.Size * _viewModel.Size;
        foreach (var array in _viewModel.SingleTileControlViewModel)
        {
            foreach (var item in array)
            {
                item.Value = string.Empty;
                item.Color = new SolidColorBrush(Colors.White);
            }
        }
    }
}
