namespace Aton.UserManagement.Dal.Models;

public record UserModel
{
    public string Login { get; init; }

    public string Password { get; init; }

    public string Name { get; init; }

    public int Gender { get; init; }

    public DateTime? Birthday { get; init; }

    public bool Admin { get; init; }
}