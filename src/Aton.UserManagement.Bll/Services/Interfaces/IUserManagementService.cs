using Aton.UserManagement.Bll.Models;

namespace Aton.UserManagement.Bll.Services.Interfaces;

public interface IUserManagementService
{
    Task<int> Create(
        string principalLogin,
        UserModel user,
        CancellationToken cancellationToken);

    Task<bool> IsLoginFree(
        string login,
        CancellationToken cancellationToken);

    Task<UserModel?> GetUserByLogin(
        string login,
        CancellationToken cancellationToken);

    Task<UserModel[]> GetActiveUsers(
        CancellationToken cancellationToken);

    Task<UserModel[]> GetOlderThan(
        int age,
        CancellationToken cancellationToken);

    Task Delete(
        string login,
        CancellationToken cancellationToken);

    Task Revoke(
        string revokerLogin,
        string login,
        CancellationToken cancellationToken);

    Task Restore(
        string restorerLogin,
        string login,
        CancellationToken cancellationToken);

    Task UpdateUserInfo(
        string modifierLogin,
        string userLogin,
        string name,
        int gender,
        DateTime? birthday,
        CancellationToken cancellationToken);

    Task UpdateUserPassword(
        string modifierLogin,
        string userLogin,
        string password,
        CancellationToken cancellationToken);

    Task UpdateUserLogin(
        string modifierLogin,
        string oldLogin,
        string newLogin,
        CancellationToken cancellationToken);
}