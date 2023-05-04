using Aton.UserManagement.Bll.Models;

namespace Aton.UserManagement.Bll.Services.Interfaces;

public interface IUserManagementService
{
    Task<int> Create(string principalLogin, UserModel user, CancellationToken cancellationToken);
    Task<bool> IsLoginFree(string login, CancellationToken cancellationToken);
    Task<UserModel?> GetUserByLogin(string login, CancellationToken cancellationToken);
    Task<UserModel[]> GetActiveUsers(CancellationToken cancellationToken);
    Task<UserModel[]> GetOlderThan(int age, CancellationToken cancellationToken);
    Task Delete(string login, CancellationToken cancellationToken);
    Task Revoke(string revokerLogin, string login, CancellationToken cancellationToken);
}