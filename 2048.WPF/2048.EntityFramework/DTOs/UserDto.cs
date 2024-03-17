namespace _2048.EntityFramework.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }

}
