using _2048.Domain.Commands;
using _2048.Domain.Models;
using _2048.Domain.Queries;

namespace _2048.WPF.Stores;

public class LeaderboardStore
{
    public static LeaderboardStore? Instance { get; set; }

    private List<LeaderboardEntry> _entries = [];

    public IEnumerable<LeaderboardEntry> Entries => _entries;

    private readonly IGetAllLeaderboardEntriesQuery _query;
    private readonly ICreateLeaderboardEntryCommand _createLeaderboardEntryCommand;

    public event Action? LeaderboardRead;
    public event Action<LeaderboardEntry>? LeaderboardEntryCreated;

    public static void CreateInstance(IGetAllLeaderboardEntriesQuery getAllLeaderboardEntriesQuery, ICreateLeaderboardEntryCommand createLeaderboardEntryCommand)
    {
        Instance ??= new LeaderboardStore(getAllLeaderboardEntriesQuery, createLeaderboardEntryCommand);
    }
    private LeaderboardStore(IGetAllLeaderboardEntriesQuery getAllLeaderboardEntries, ICreateLeaderboardEntryCommand createLeaderboardEntryCommand)
    {
        _query = getAllLeaderboardEntries;
        _createLeaderboardEntryCommand = createLeaderboardEntryCommand;
    }

    public async Task Read()
    {
        IEnumerable<LeaderboardEntry> entries = await _query.ExecuteAsync();

        _entries.Clear();

        _entries.AddRange(entries);

        LeaderboardRead?.Invoke();
    }

    public async Task Create(LeaderboardEntry leaderboardEntry)
    {
        await _createLeaderboardEntryCommand.ExecuteAsync(leaderboardEntry);

        _entries.Add(leaderboardEntry);

        LeaderboardEntryCreated?.Invoke(leaderboardEntry);
    }
}
