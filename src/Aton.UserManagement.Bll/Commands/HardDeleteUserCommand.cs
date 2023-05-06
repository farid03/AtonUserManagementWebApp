using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Commands;

public record HardDeleteUserCommand(
        Principal Principal,
        string Login)
    : IRequest;

public class HardDeleteUserCommandHandler
    : IRequestHandler<HardDeleteUserCommand>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserManagementService _userManagementService;

    public HardDeleteUserCommandHandler(
        IUserManagementService userManagementService,
        IAuthorizationService authorizationService)
    {
        _userManagementService = userManagementService;
        _authorizationService = authorizationService;
    }

    public async Task Handle(
        HardDeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsAdminUser(command.Principal, cancellationToken))
            throw new ForbiddenException();

        if (await _userManagementService.IsLoginFree(command.Login, cancellationToken))
            throw new UserNotFoundException();
        
        await _userManagementService.Delete(
            command.Login,
            cancellationToken);
    }
}