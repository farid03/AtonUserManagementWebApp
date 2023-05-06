using Aton.UserManagement.Bll.Models;

namespace Aton.UserManagement.Api.Requests.V1;

public record UpdateUserPasswordRequest(
    Principal Principal,
    string UserLogin,
    string NewPassword
);