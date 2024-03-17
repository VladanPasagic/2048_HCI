using _2048.Domain.Models;

namespace _2048.Domain.Queries;

public interface IGetAllLeaderboardEntriesQuery
{
    Task<IEnumerable<LeaderboardEntry>> ExecuteAsync();
}
