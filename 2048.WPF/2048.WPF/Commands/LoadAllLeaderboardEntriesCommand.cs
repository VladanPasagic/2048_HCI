
using _2048.WPF.Stores;
using _2048.WPF.ViewModels;

namespace _2048.WPF.Commands;

public class LoadAllLeaderboardEntriesCommand : AsyncCommandBase
{
    private readonly ILoadingViewModel _loadingViewModel;
    private readonly IErrorViewModel _errorViewModel;

    public LoadAllLeaderboardEntriesCommand(ILoadingViewModel loadingViewModel, IErrorViewModel errorViewModel)
    {
        _loadingViewModel = loadingViewModel;
        _errorViewModel = errorViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        try
        {
            await LeaderboardStore.Instance!.Read();
        }
        catch (Exception)
        {

        }
    }
}
