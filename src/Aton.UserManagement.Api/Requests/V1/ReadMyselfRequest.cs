using Aton.UserManagement.Api.Requests.V1.Common;

namespace Aton.UserManagement.Api.Requests.V1;

public record ReadMyselfRequest(
    Principal Principal
);