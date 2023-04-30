namespace Aton.UserManagement.Bll.Exceptions;

public class LoginAlreadyExistsException : Exception
{
    public LoginAlreadyExistsException() : base("Пользователь с данным логином уже существует")
    {
    }
}