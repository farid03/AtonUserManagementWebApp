namespace Aton.UserManagement.Bll.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException() : base("Для совершения операции требуется привелегия администратора")
    {
    }
}