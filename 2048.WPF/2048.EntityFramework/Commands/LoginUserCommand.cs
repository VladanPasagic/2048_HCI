using _2048.Domain.Commands;
using _2048.Domain.Models;

namespace _2048.EntityFramework.Commands;

public class LoginUserCommand : ILoginUserCommand
{
    private readonly _2048DbContextFactory _contextFactory;

    public LoginUserCommand(_2048DbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public (bool, string) Execute(User user)
    {
        using _2048DbContext context = _contextFactory.Create();
        var userDto = context.Users.FirstOrDefault(u => u.Username.Equals(user.Username));
        if (userDto is null || !BCrypt.Net.BCrypt.Verify(user.PasswordHash, userDto.PasswordHash))
        {
            return (false, "Invalid username or password");
        }
        return (true, string.Empty);
    }
}
