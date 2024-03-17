using _2048.Domain.Models;
using _2048.Domain.Queries;
using Microsoft.EntityFrameworkCore;

namespace _2048.EntityFramework.Queries;

public class GetAllLeaderboardEntriesQuery : IGetAllLeaderboardEntriesQuery
{
    private readonly _2048DbContextFactory _contextFactory;

    public GetAllLeaderboardEntriesQuery(_2048DbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<LeaderboardEntry>> ExecuteAsync()
    {
        using var context = _contextFactory.Create();

        var lbEntries = await context.LeaderboardEntries.Include(le => le.User).ToListAsync();

        return lbEntries.OrderByDescending(le => le.Score).Select(le => new LeaderboardEntry()
        {
            Id = le.Id,
            Score = le.Score,
            User = new(le.User.Id, le.User.Username)
        });
    }
}
