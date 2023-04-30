using Aton.UserManagement.Bll.Exceptions;

namespace Aton.UserManagement.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            if (exception is UserNotFoundException or LoginAlreadyExistsException or InvalidPrincipalCreditsException)
                response.StatusCode = 400;
            if (exception is ForbiddenException)
                response.StatusCode = 403;
            else
                response.StatusCode = 500;
            
            context.Response.ContentType = "text/plain";
            await response.WriteAsync($"{exception.GetType().FullName} {exception.Message}");
            
            // throw;
        }
    }
}