namespace Aton.UserManagement.Api.Responses.V1;

public record ReadUserByLoginResponse(
    string Name,
    int Gender,
    DateTime? Birthday,
    bool IsActive
);