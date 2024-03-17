using _2048.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace _2048.EntityFramework;

public class _2048DbContext : DbContext
{
    public _2048DbContext(DbContextOptions options) : base(options) { }

    public DbSet<UserDto> Users { get; set; }

    public DbSet<LeaderboardEntryDto> LeaderboardEntries { get; set; }
}
