namespace Aton.UserManagement.Api.Requests.V1;

public record CreateUserRequest(
    string Login,
    string Password,
    CreateUserRequest.User UserToCreate
)
{
    public record User(
        string Login,
        string Password,
        string Name,
        int Gender,
        DateTime? Birthday,
        bool Admin
    );
}