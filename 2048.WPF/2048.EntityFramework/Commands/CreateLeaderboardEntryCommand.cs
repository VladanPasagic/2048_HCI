using _2048.Domain.Commands;
using _2048.Domain.Models;
using _2048.EntityFramework.DTOs;

namespace _2048.EntityFramework.Commands;

public class CreateLeaderboardEntryCommand : ICreateLeaderboardEntryCommand
{
    private readonly _2048DbContextFactory _contextFactory;

    public CreateLeaderboardEntryCommand(_2048DbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task ExecuteAsync(LeaderboardEntry leaderboardEntry)
    {
        using var context = _contextFactory.Create();

        var user = context.Users.FirstOrDefault(u => u.Username == leaderboardEntry.User.Username);

        context.LeaderboardEntries.Add(new LeaderboardEntryDto()
        {
            User = user!,
            Score = leaderboardEntry.Score,
            Id = leaderboardEntry.Id
        });

        await context.SaveChangesAsync();
    }
}
