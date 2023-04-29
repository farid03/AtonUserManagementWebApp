using Aton.UserManagement.Bll.Models;

namespace Aton.UserManagement.Bll.Services.Interfaces;

public interface IUserManagementService
{
    Task<int> Create(string ownerLogin, UserModel user, CancellationToken cancellationToken);
}