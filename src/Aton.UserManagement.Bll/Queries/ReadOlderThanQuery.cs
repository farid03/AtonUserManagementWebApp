using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Queries;

public record ReadOlderThanQuery(
        Principal Principal,
        int Age)
    : IRequest<UserModel[]>;

public class ReadOlderThanQueryHandler
    : IRequestHandler<ReadOlderThanQuery, UserModel[]>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserManagementService _userManagementService;

    public ReadOlderThanQueryHandler(
        IUserManagementService userManagementService,
        IAuthorizationService authorizationService)
    {
        _userManagementService = userManagementService;
        _authorizationService = authorizationService;
    }

    public async Task<UserModel[]> Handle(
        ReadOlderThanQuery query,
        CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsAdminUser(query.Principal, cancellationToken))
            throw new ForbiddenException();

        if (!await _authorizationService.IsActivePrincipal(query.Principal, cancellationToken))
            throw new ForbiddenException();

        var user = await _userManagementService.GetOlderThan(query.Age, cancellationToken);

        return user;
    }
}