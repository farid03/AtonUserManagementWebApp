using Aton.UserManagement.Bll.Extensions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Commands;

public record CreateUserCommand(
        string OwnerLogin,
        UserModel User)
    : IRequest<int>;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserManagementService _userManagementService;

    public CreateUserCommandHandler(
        IUserManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    public async Task<int> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        command
            .EnsureIsAdminUser()
            .EnsureUniqLogin();

        var user = new UserModel(
            command.User.Login,
            command.User.Password,
            command.User.Name,
            command.User.Gender,
            command.User.Birthday,
            command.User.Admin
        );

        var userId = await _userManagementService.Create(
            command.OwnerLogin,
            user,
            cancellationToken);

        return userId;
    }
}