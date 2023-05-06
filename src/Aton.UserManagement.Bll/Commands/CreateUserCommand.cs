using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Commands;

public record CreateUserCommand(
        Principal Principal,
        UserModel User)
    : IRequest<int>;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, int>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserManagementService _userManagementService;

    public CreateUserCommandHandler(
        IUserManagementService userManagementService,
        IAuthorizationService authorizationService)
    {
        _userManagementService = userManagementService;
        _authorizationService = authorizationService;
    }

    public async Task<int> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsAdminUser(command.Principal, cancellationToken))
            throw new ForbiddenException();

        if (!await _userManagementService.IsLoginFree(command.User.Login, cancellationToken))
            throw new LoginAlreadyExistsException();

        var user = new UserModel(
            command.User.Login,
            command.User.Password,
            command.User.Name,
            command.User.Gender,
            command.User.Birthday,
            command.User.Admin,
            command.User.IsActive
        );

        var userId = await _userManagementService.Create(
            command.Principal.Login,
            user,
            cancellationToken);

        return userId;
    }
}