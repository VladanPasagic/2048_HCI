namespace _2048.Domain.Models;

public class LeaderboardEntry
{
    public User User { get; set; }

    public Guid Id { get; set; }

    public int Score { get; set; }
}
