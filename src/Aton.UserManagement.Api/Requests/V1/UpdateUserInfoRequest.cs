using Aton.UserManagement.Bll.Models;

namespace Aton.UserManagement.Api.Requests.V1;

public record UpdateUserInfoRequest(
    Principal Principal,
    string UserLogin,
    string Name,
    int Gender,
    DateTime? Birthday
);