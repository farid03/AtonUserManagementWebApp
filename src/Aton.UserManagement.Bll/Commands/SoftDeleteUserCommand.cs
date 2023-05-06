using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Commands;

public record SoftDeleteUserCommand(
        Principal Principal,
        string Login)
    : IRequest;

public class SoftDeleteUserCommandHandler
    : IRequestHandler<SoftDeleteUserCommand>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserManagementService _userManagementService;

    public SoftDeleteUserCommandHandler(
        IUserManagementService userManagementService,
        AuthorizationService authorizationService)
    {
        _userManagementService = userManagementService;
        _authorizationService = authorizationService;
    }

    public async Task Handle(
        SoftDeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsAdminUser(command.Principal, cancellationToken))
            throw new ForbiddenException();

        await _userManagementService.Revoke(
            command.Principal.Login,
            command.Login,
            cancellationToken);
    }
}