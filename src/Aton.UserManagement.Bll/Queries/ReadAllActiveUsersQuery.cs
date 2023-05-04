using Aton.UserManagement.Bll.Commands;
using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Queries;

public record ReadAllActiveUsersQuery(
        Principal Principal)
    : IRequest<UserModel[]>;

public class ReadAllActiveUsersQueryHandler
    : IRequestHandler<ReadAllActiveUsersQuery, UserModel[]>
{
    private readonly IUserManagementService _userManagementService;
    private readonly AuthorizationService _authorizationService;

    public ReadAllActiveUsersQueryHandler(
        IUserManagementService userManagementService,
        AuthorizationService authorizationService)
    {
        _userManagementService = userManagementService;
        _authorizationService = authorizationService;
    }

    public async Task<UserModel[]> Handle(ReadAllActiveUsersQuery query, CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsAdminUser(query.Principal, cancellationToken))
            throw new ForbiddenException();

        if (!await _authorizationService.IsActivePrincipal(query.Principal, cancellationToken))
            throw new ForbiddenException();
        
        var user = await _userManagementService.GetActiveUsers(cancellationToken);

        return user;
    }
}