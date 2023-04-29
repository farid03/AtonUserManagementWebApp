namespace Aton.UserManagement.Dal.Settings;

public record DalOptions
{
    public string ConnectionString { get; init; } = string.Empty;
}