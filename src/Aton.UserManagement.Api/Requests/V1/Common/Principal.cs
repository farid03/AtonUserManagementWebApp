namespace Aton.UserManagement.Api.Requests.V1.Common;

public record Principal(
    string Login,
    string Password
);