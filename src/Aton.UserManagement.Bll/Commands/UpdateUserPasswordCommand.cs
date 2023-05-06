using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Commands;

public record UpdateUserPasswordCommand(
    Principal Principal,
    string UserLogin,
    string Password
) : IRequest;

public class UpdateUserPasswordCommandHandler
    : IRequestHandler<UpdateUserPasswordCommand>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserManagementService _userManagementService;

    public UpdateUserPasswordCommandHandler(
        IUserManagementService userManagementService,
        IAuthorizationService authorizationService)
    {
        _userManagementService = userManagementService;
        _authorizationService = authorizationService;
    }

    public async Task Handle(
        UpdateUserPasswordCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsAdminUser(command.Principal, cancellationToken)
            && !(await _authorizationService.IsActivePrincipal(command.Principal, cancellationToken)
                 && command.Principal.Login.Equals(command.UserLogin)))
            throw new ForbiddenException();

        if (await _userManagementService.IsLoginFree(command.UserLogin, cancellationToken))
            throw new UserNotFoundException();
        
        await _userManagementService.UpdateUserPassword(
            command.Principal.Login,
            command.UserLogin,
            command.Password,
            cancellationToken);
    }
}