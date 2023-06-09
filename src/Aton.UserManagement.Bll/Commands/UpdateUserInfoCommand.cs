using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Commands;

public record UpdateUserInfoCommand(
    Principal Principal,
    string UserLogin,
    string Name,
    int Gender,
    DateTime? Birthday
) : IRequest;

public class UpdateUserInfoCommandHandler
    : IRequestHandler<UpdateUserInfoCommand>
{
    private readonly IUserManagementService _userManagementService;
    private readonly IAuthorizationService _authorizationService;

    public UpdateUserInfoCommandHandler(
        IUserManagementService userManagementService,
        IAuthorizationService authorizationService)
    {
        _userManagementService = userManagementService;
        _authorizationService = authorizationService;
    }

    public async Task Handle(
        UpdateUserInfoCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsAdminUser(command.Principal, cancellationToken)
            && !(await _authorizationService.IsActivePrincipal(command.Principal, cancellationToken)
                 && command.Principal.Login.Equals(command.UserLogin)))
            throw new ForbiddenException();

        if (await _userManagementService.IsLoginFree(command.UserLogin, cancellationToken))
            throw new UserNotFoundException();
        
        await _userManagementService.UpdateUserInfo(
            command.Principal.Login,
            command.UserLogin,
            command.Name,
            command.Gender,
            command.Birthday,
            cancellationToken);
    }
}