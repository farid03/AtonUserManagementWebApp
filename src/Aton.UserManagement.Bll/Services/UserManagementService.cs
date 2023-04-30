using System.Security.Cryptography;
using Aton.UserManagement.Bll.Models;
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
        string principalLogin,
        UserModel user,
        CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();

        var newUser = new Dal.Models.UserModel()
        {
            Login = user.Login,
            Password = ComputeHash(user.Password),
            Name = user.Name,
            Gender = user.Gender,
            Birthday = user.Birthday,
            Admin = user.Admin,
        };

        var guid = await _usersRepository.Add(principalLogin, newUser, cancellationToken);

        transaction.Complete();

        return guid;
    }

    public async Task<bool> IsLoginFree(
        string login,
        CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        var user = await _usersRepository.Get(login, cancellationToken);
        transaction.Complete();

        if (user is not null)
            return true;

        return false;
    }
    
    private string ComputeHash(string inputString)
    {
        using var md5 = MD5.Create();

        var data = System.Text.Encoding.ASCII.GetBytes(inputString);
        data = md5.ComputeHash(data);
        var hash = Convert.ToBase64String(data);

        return hash;
    }
}