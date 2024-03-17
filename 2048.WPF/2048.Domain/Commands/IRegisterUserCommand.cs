using _2048.Domain.Models;

namespace _2048.Domain.Commands;

public interface IRegisterUserCommand
{
    Task<(bool, string)> Execute(User user);
}
