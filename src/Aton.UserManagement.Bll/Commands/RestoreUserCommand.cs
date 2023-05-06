using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Extensions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Commands;

public record RestoreUserCommand(
        Principal Principal,
        string Login)
    : IRequest;

public class RestoreUserCommandHandler
    : IRequestHandler<RestoreUserCommand>
{
    private readonly IUserManagementService _userManagementService;
    private readonly IAuthorizationService _authorizationService;

    public RestoreUserCommandHandler(
        IUserManagementService userManagementService,
        AuthorizationService authorizationService)
    {
        _userManagementService = userManagementService;
        _authorizationService = authorizationService;
    }

    public async Task Handle(
        RestoreUserCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsAdminUser(command.Principal, cancellationToken))
            throw new ForbiddenException();
        
        await _userManagementService.Restore(
            command.Principal.Login,
            command.Login,
            cancellationToken);
    }
}