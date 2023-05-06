namespace Aton.UserManagement.Bll.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("Пользователь не найден")
    {
    }
}