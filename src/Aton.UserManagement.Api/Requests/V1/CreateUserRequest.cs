using Aton.UserManagement.Api.Requests.V1.Common;

namespace Aton.UserManagement.Api.Requests.V1;

public record CreateUserRequest(
    Principal Principal,
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