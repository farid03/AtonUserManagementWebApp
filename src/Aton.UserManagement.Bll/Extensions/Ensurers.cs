using Aton.UserManagement.Bll.Commands;
using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Dal.Entities;
using Aton.UserManagement.Dal.Repositories.Interfaces;

namespace Aton.UserManagement.Bll.Extensions;

public class UserEnsurersBuilder
{
    public UserEnsurersBuilder()
    {
        
    }   
    
    public UserEntityV1 EnsureUniqLogin(
        CreateUserCommand src)
    {
        // if (!src.Goods.Any()) throw new GoodsNotFoundException();
        // TODO проверить что логин уникальный
        return new UserEntityV1();
    }
}