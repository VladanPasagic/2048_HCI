using System.Security;

namespace _2048.Domain.Models;

public class User
{
    public User(Guid id, string username)
    {
        Id = id;
        Username = username;
        PasswordHash = string.Empty;
    }
    public User(Guid id, string username, string passwordHash)
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
    }

    public Guid Id { get; }

    public string Username { get; }

    public string PasswordHash { get; }
}
