using System.Security.Cryptography;
using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Dal.Repositories.Interfaces;

namespace Aton.UserManagement.Bll.Services;

// TODO выделить абстракцию
public class AuthorizationService
{
    private readonly IUsersRepository _usersRepository;

    public AuthorizationService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<bool> IsAdminUser(
        Principal principal,
        CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        var user = await _usersRepository.Get(principal.Login, cancellationToken);
        transaction.Complete();

        if (user is null || !IsPasswordEqual(principal.Password, user.Password))
            throw new InvalidPrincipalCreditsException();
        
        if (user.Admin)
            return true;
        
        return false;
    }
    
    public async Task<bool> IsValidPrincipal(
        Principal principal,
        CancellationToken cancellationToken)
    {
        using var transaction = _usersRepository.CreateTransactionScope();
        var user = await _usersRepository.Get(principal.Login, cancellationToken);
        transaction.Complete();
        
        return user is not null && IsPasswordEqual(principal.Password, user.Password);
    }

    private bool IsPasswordEqual(string password, string passwordHash)
    {
        return passwordHash.Equals(ComputeHash(password));
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