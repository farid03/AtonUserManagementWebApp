using Aton.UserManagement.Bll.Commands;
using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Dal.Repositories.Interfaces;

namespace Aton.UserManagement.Bll.Extensions;

public static class Ensurers
{
    public static CreateUserCommand EnsureUniqLogin(
        this CreateUserCommand src)
    {
        // if (!src.Goods.Any()) throw new GoodsNotFoundException();
        // TODO проверить что логин уникальный
        return src;
    }
}