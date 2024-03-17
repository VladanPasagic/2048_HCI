using _2048.Domain.Models;

namespace _2048.Domain.Commands;

public interface ILoginUserCommand
{
    (bool, string) Execute(User user);
}
