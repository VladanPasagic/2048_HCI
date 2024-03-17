using _2048.Domain.Commands;
using _2048.Domain.Models;
using _2048.EntityFramework.DTOs;

namespace _2048.EntityFramework.Commands;

public class RegisterUserCommand : IRegisterUserCommand
{
    private readonly _2048DbContextFactory _contextFactory;

    public RegisterUserCommand(_2048DbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<(bool, string)> Execute(User user)
    {
        using _2048DbContext context = _contextFactory.Create();
        if (context.Users.Any(u => u.Username.Equals(user.Username)))
        {
            return (false, "A user with that username already exists");
        }
        UserDto userDto = new()
        {
            Id = user.Id,
            Username = user.Username,
            PasswordHash = user.PasswordHash
        };
        context.Users.Add(userDto);
        await context.SaveChangesAsync();
        return (true, string.Empty);
    }
}
