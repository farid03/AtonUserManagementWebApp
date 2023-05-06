namespace Aton.UserManagement.Bll.Exceptions;

public class InvalidPrincipalCreditsException : Exception
{
    public InvalidPrincipalCreditsException() : base("Неправильный логин или пароль пользователя")
    {
    }
}