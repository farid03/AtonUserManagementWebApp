using Aton.UserManagement.Bll.Services.Interfaces;
using Aton.UserManagement.Dal.Entities;
using Aton.UserManagement.Dal.Repositories;
using Aton.UserManagement.Dal.Repositories.Interfaces;
using UserModel = Aton.UserManagement.Bll.Models.UserModel;

namespace Aton.UserManagement.Bll.Services;

public class UserManagementService : IUserManagementService
{
    private readonly IUsersRepository _usersRepository;

    public UserManagementService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task<int> Create(
        string ownerLogin,
        UserModel user,
        CancellationToken cancellationToken
    )
    {
        using var transaction = _usersRepository.CreateTransactionScope();

        var newUser = new Dal.Models.UserModel()
        {
            Login = user.Login,
            Password = user.Password,
            Name = user.Name,
            Gender = user.Gender,
            Birthday = user.Birthday,
            Admin = user.Admin,
        };

        var guid = await _usersRepository.Add(ownerLogin, newUser, cancellationToken);

        transaction.Complete();

        return guid;
    }
}