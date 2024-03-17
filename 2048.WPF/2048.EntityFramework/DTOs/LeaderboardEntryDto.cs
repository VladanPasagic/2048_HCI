namespace _2048.EntityFramework.DTOs;

public class LeaderboardEntryDto
{
    public UserDto User { get; set; } = null!;

    public Guid Id { get; set; }

    public int Score { get; set; }
}
