using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Commands;

public record UpdateUserLoginCommand(
    Principal Principal,
    string OldLogin,
    string NewLogin
) : IRequest;

public class UpdateUserLoginCommandHandler
    : IRequestHandler<UpdateUserLoginCommand>
{
    private readonly IUserManagementService _userManagementService;
    private readonly AuthorizationService _authorizationService;

    public UpdateUserLoginCommandHandler(
        IUserManagementService userManagementService,
        AuthorizationService authorizationService)
    {
        _userManagementService = userManagementService;
        _authorizationService = authorizationService;
    }

    public async Task Handle(
        UpdateUserLoginCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsAdminUser(command.Principal, cancellationToken)
            && !(await _authorizationService.IsActivePrincipal(command.Principal, cancellationToken)
                 && command.Principal.Login.Equals(command.OldLogin)))
            throw new ForbiddenException();

        if (!await _userManagementService.IsLoginFree(command.NewLogin, cancellationToken))
            throw new LoginAlreadyExistsException();

        await _userManagementService.UpdateUserLogin(
            command.Principal.Login,
            command.OldLogin,
            command.NewLogin,
            cancellationToken);
    }
}