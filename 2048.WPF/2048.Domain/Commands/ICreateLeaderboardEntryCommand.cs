using _2048.Domain.Models;

namespace _2048.Domain.Commands;

public interface ICreateLeaderboardEntryCommand
{
    Task ExecuteAsync(LeaderboardEntry leaderboardEntry);
}
