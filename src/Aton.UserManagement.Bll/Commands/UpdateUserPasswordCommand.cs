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
    private readonly IUserManagementService _userManagementService;
    private readonly AuthorizationService _authorizationService;

    public UpdateUserPasswordCommandHandler(
        IUserManagementService userManagementService,
        AuthorizationService authorizationService)
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

        await _userManagementService.UpdateUserPassword(
            command.Principal.Login,
            command.UserLogin,
            command.Password,
            cancellationToken);
    }
}