using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Queries;

public record ReadMyselfQuery(
        Principal Principal)
    : IRequest<UserModel>;

public class ReadMyselfQueryHandler
    : IRequestHandler<ReadMyselfQuery, UserModel>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserManagementService _userManagementService;

    public ReadMyselfQueryHandler(
        IUserManagementService userManagementService,
        IAuthorizationService authorizationService)
    {
        _userManagementService = userManagementService;
        _authorizationService = authorizationService;
    }

    public async Task<UserModel> Handle(
        ReadMyselfQuery query,
        CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsActivePrincipal(query.Principal, cancellationToken))
            throw new ForbiddenException();

        var user = await _userManagementService.GetUserByLogin(
            query.Principal.Login,
            cancellationToken);

        if (user is null)
            throw new UserNotFoundException();

        return user;
    }
}