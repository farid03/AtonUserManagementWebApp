using Aton.UserManagement.Bll.Models;

namespace Aton.UserManagement.Bll.Services.Interfaces;

public interface IUserManagementService
{
    Task<int> Create(string principalLogin, UserModel user, CancellationToken cancellationToken);
    Task<bool> IsLoginFree(string login, CancellationToken cancellationToken);
}