namespace Aton.UserManagement.Bll.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException() : base("Отказано в доступе")
    {
    }
}