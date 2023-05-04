namespace Aton.UserManagement.Api.Responses.V1;

public record ReadAllActiveUsersResponse(
    ReadAllActiveUsersResponse.User[] Users
)
{
    public record User(
        string Name,
        int Gender,
        DateTime? Birthday,
        bool IsAdmin
    );
}