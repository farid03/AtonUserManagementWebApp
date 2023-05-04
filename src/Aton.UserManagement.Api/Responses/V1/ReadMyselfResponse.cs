namespace Aton.UserManagement.Api.Responses.V1;

public record ReadMyselfResponse(
    string Name,
    int Gender,
    DateTime? Birthday,
    bool IsActive
);