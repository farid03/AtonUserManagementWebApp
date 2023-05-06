using Aton.UserManagement.Bll.Models;

namespace Aton.UserManagement.Api.Requests.V1;

public record UpdateUserLoginRequest(
    Principal Principal,
    string OldLogin,
    string NewLogin
);