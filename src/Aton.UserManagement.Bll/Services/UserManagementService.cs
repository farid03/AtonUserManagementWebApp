using System.Security.Cryptography;
using Aton.UserManagement.Bll.Services.Interfaces;
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

        return user is null;
    }

    public async Task<UserModel?> GetUserByLogin(string login, CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        var user = await _usersRepository.Get(login, cancellationToken);
        transaction.Complete();

        if (user is null)
            return null;

        var result = new UserModel(
            user.Login,
            user.Password,
            user.Name,
            user.Gender,
            user.Birthday,
            user.Admin,
            user.RevokedOn is null
        );

        return result;
    }
    
    public async Task<UserModel[]> GetActiveUsers(CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        var users = await _usersRepository.GetAllActiveUsers(cancellationToken);
        transaction.Complete();

        var result = users
            .Select( x => 
                new UserModel(
                    x.Login, 
                    x.Password, 
                    x.Name, 
                    x.Gender, 
                    x.Birthday, 
                    x.Admin, 
                    x.RevokedOn is null
                    )
            )
            .ToArray();

        return result;
    }
    
    public async Task<UserModel[]> GetOlderThan(int age, CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        var users = await _usersRepository.GetOlderThan(age, cancellationToken);
        transaction.Complete();

        var result = users
            .Select( x => 
                new UserModel(
                    x.Login, 
                    x.Password, 
                    x.Name, 
                    x.Gender, 
                    x.Birthday, 
                    x.Admin, 
                    x.RevokedOn is null
                )
            )
            .ToArray();

        return result;
    }

    public async Task Delete(string login, CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        await _usersRepository.Delete(login, cancellationToken);
        transaction.Complete();
    }
    
    public async Task Revoke(string revokerLogin, string login, CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        await _usersRepository.Revoke(revokerLogin, login, cancellationToken);
        transaction.Complete();
    }

    public async Task Restore(string restorerLogin, string login, CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        await _usersRepository.Restore(restorerLogin, login, cancellationToken);
        transaction.Complete();
    }

    public async Task UpdateUserInfo(string modifierLogin, string userLogin, string name, int gender, DateTime? birthday,
        CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        await _usersRepository.UpdateUserInfo(modifierLogin, userLogin, name, gender, birthday, cancellationToken);
        transaction.Complete();
    }

    public async Task UpdateUserPassword(string modifierLogin, string userLogin, string password, CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        await _usersRepository.UpdateUserPassword(modifierLogin, userLogin, ComputeHash(password), cancellationToken);
        transaction.Complete();    
    }

    public async Task UpdateUserLogin(string modifierLogin, string oldLogin, string newLogin, CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        await _usersRepository.UpdateUserLogin(modifierLogin, oldLogin, newLogin, cancellationToken);
        transaction.Complete();
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