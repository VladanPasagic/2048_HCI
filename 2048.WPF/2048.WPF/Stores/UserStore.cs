using _2048.Domain.Commands;
using _2048.Domain.Models;
using _2048.EntityFramework.DTOs;

namespace _2048.WPF.Stores;

public class UserStore
{
    public static UserStore? Instance { get; set; }

    public static void CreateInstance(ILoginUserCommand loginUserCommand, IRegisterUserCommand registerUserCommand)
    {
        Instance ??= new(loginUserCommand, registerUserCommand);
    }

    private readonly ILoginUserCommand _loginUserCommand;
    private readonly IRegisterUserCommand _registerUserCommand;

    private readonly List<User> _users = [];

    public IEnumerable<User> Users => _users;

    private UserStore(ILoginUserCommand loginUserCommand, IRegisterUserCommand registerUserCommand)
    {
        _loginUserCommand = loginUserCommand;
        _registerUserCommand = registerUserCommand;
    }

    private User? _activeUser;

    public User? ActiveUser
    {
        get => _activeUser;
        set => _activeUser = value;
    }

    public async Task<(bool, string)> Register(User user)
    {
        return await _registerUserCommand.Execute(user);
    }

    public (bool, string) Login(User user)
    {
        return _loginUserCommand.Execute(user);
    }
}
