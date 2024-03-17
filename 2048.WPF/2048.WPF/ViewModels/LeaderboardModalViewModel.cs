using _2048.Domain.Models;
using _2048.WPF.Commands;
using _2048.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace _2048.WPF.ViewModels;

public class LeaderboardModalViewModel : ViewModelBase, IErrorViewModel, ILoadingViewModel
{
    public ObservableCollection<LeaderboardEntry> Entries { get; } = [];

    public LeaderboardModalViewModel(ICommand closeModalCommand)
    {
        LeaderboardStore.Instance!.LeaderboardRead += LeaderboardModalViewModel_LeaderboardRead;
        LeaderboardStore.Instance!.LeaderboardEntryCreated += LeaderboardModalViewModel_LeaderboardEntryCreated;

        new LoadAllLeaderboardEntriesCommand(this, this).Execute(null);

        CloseModalCommand = closeModalCommand;
    }

    private void LeaderboardModalViewModel_LeaderboardEntryCreated(LeaderboardEntry entry)
    {
        Entries.Add(entry);
        var entries = Entries.OrderByDescending(le => le.Score).ToList();

        Entries.Clear();
        foreach (var singleEntry in entries)
        {
            Entries.Add(singleEntry);
        }
    }

    public override void Dispose()
    {
        LeaderboardStore.Instance!.LeaderboardRead -= LeaderboardModalViewModel_LeaderboardRead;
        LeaderboardStore.Instance!.LeaderboardEntryCreated -= LeaderboardModalViewModel_LeaderboardEntryCreated;

        base.Dispose();
    }

    private void LeaderboardModalViewModel_LeaderboardRead()
    {
        Entries.Clear();

        foreach (var entry in LeaderboardStore.Instance!.Entries)
        {
            Entries.Add(entry);
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

    private bool _isLoading = false;
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public ICommand CloseModalCommand { get; }
}
