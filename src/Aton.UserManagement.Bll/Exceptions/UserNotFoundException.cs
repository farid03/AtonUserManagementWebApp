namespace Aton.UserManagement.Bll.Exceptions;

// TODO возможно придется добавлять другие типы исключений
public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("Пользователь не найден")
    {
    }
}