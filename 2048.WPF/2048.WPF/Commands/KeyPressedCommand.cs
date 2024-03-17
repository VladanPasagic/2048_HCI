using _2048.WPF.ViewModels;
using System.Windows.Input;

namespace _2048.WPF.Commands;

public class KeyPressedCommand : CommandBase
{
    private readonly _2048ViewModel _viewModel;
    private readonly Key _key;

    public KeyPressedCommand(_2048ViewModel viewModel, Key key)
    {
        _viewModel = viewModel;
        _key = key;
    }

    public override void Execute(object? parameter)
    {
        var matrix = _viewModel.SingleTileControlViewModel;

        if (!_viewModel.HasStarted)
            return;

        lock (_viewModel.lockObject)
        {
            List<List<string>> mat = [];
            foreach (var array in matrix)
            {
                List<string> arr = [];
                foreach (var item in array)
                {
                    arr.Add(item.Value);
                }
                mat.Add(arr);
            }

            if (_key == Key.W || _key == Key.Up)
            {
                for (int i = 0; i < _viewModel.Size; i++)
                {
                    _viewModel.MoveUp(i);
                    for (int j = 1; j < _viewModel.Size; j++)
                    {
                        if (_viewModel.MergeAndDouble(matrix[i][j - 1], matrix[i][j]).Result)
                        {
                            j++;
                        }
                    }
                    _viewModel.MoveUp(i);
                }
            }
            else if (_key == Key.A || _key == Key.Left)
            {
                for (int j = 0; j < _viewModel.Size; j++)
                {
                    _viewModel.MoveLeft(j);
                    for (int i = 1; i < _viewModel.Size; i++)
                    {
                        if (_viewModel.MergeAndDouble(matrix[i - 1][j], matrix[i][j]).Result)
                        {
                            i++;
                        }
                    }
                    _viewModel.MoveLeft(j);
                }
            }
            else if (_key == Key.S || _key == Key.Down)
            {
                for (int i = 0; i < _viewModel.Size; i++)
                {
                    _viewModel.MoveDown(i);
                    for (int j = _viewModel.Size - 2; j >= 0; j--)
                    {
                        if (_viewModel.MergeAndDouble(matrix[i][j + 1], matrix[i][j]).Result)
                        {
                            j--;
                        }
                    }
                    _viewModel.MoveDown(i);
                }
            }
            else if (_key == Key.D || _key == Key.Right)
            {
                for (int j = 0; j < _viewModel.Size; j++)
                {
                    _viewModel.MoveRight(j);
                    for (int i = _viewModel.Size - 2; i >= 0; i--)
                    {
                        if (_viewModel.MergeAndDouble(matrix[i + 1][j], matrix[i][j]).Result)
                        {
                            i--;
                        }
                    }
                    _viewModel.MoveRight(j);
                }
            }

            if (_viewModel.CheckDidMove(mat))
            {
                _viewModel.FIllTile();
            }
            _viewModel.CheckIfCanContinue();
        }
    }
}
