using Aton.UserManagement.Dal.Entities;
using Aton.UserManagement.Dal.Models;

namespace Aton.UserManagement.Dal.Repositories.Interfaces;

public interface IUsersRepository : IDbRepository
{
    Task<int> Add(
        string ownerLogin,
        UserModel user,
        CancellationToken token);

    Task UpdateUserInfo(
        string modifierLogin,
        string userLogin,
        string name,
        int gender,
        DateTime? birthday,
        CancellationToken token);

    Task UpdateUserPassword(
        string modifierLogin,
        string userLogin,
        string password,
        CancellationToken token);

    Task UpdateUserLogin(
        string modifierLogin,
        string oldLogin,
        string newLogin,
        CancellationToken token);

    Task<UserEntityV1[]> GetAllActiveUsers(
        CancellationToken token);

    Task<UserEntityV1?> Get(
        string login,
        CancellationToken token);

    Task<UserEntityV1[]> GetOlderThan(
        int age,
        CancellationToken token);

    Task Delete(
        string login,
        CancellationToken cancellationToken);

    Task Revoke(
        string revokerLogin,
        string login,
        CancellationToken cancellationToken);

    Task Restore(
        string modifierLogin,
        string login,
        CancellationToken token);
}