using Aton.UserManagement.Bll.Models;

namespace Aton.UserManagement.Bll.Services.Interfaces;

public interface IAuthorizationService
{
    Task<bool> IsAdminUser(
        Principal principal,
        CancellationToken cancellationToken);

    Task<bool> IsValidPrincipal(
        Principal principal,
        CancellationToken cancellationToken);

    Task<bool> IsActivePrincipal(
        Principal principal,
        CancellationToken cancellationToken);
}