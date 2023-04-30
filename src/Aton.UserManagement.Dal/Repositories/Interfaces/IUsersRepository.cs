using Aton.UserManagement.Dal.Entities;
using Aton.UserManagement.Dal.Models;

namespace Aton.UserManagement.Dal.Repositories.Interfaces;

public interface IUsersRepository : IDbRepository
{
    Task<int> Add(
        string ownerLogin,
        UserModel user,
        CancellationToken token);
    
    Task<int> Update(
        string modifierLogin,
        UserModel user,
        CancellationToken token);
    
    Task<UserEntityV1[]> GetAllUsers(
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

    Task Revoke( // soft delete
        string login,
        CancellationToken cancellationToken);
    
    Task<int> Restore( // update2
        string login,
        CancellationToken token);
}