using Aton.UserManagement.Bll.Commands;
using Aton.UserManagement.Bll.Exceptions;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Queries;

public record ReadUserByLoginQuery(
        Principal Principal,
        string UserLogin)
    : IRequest<UserModel>;

public class ReadUserByLoginQueryHandler
    : IRequestHandler<ReadUserByLoginQuery, UserModel>
{
    private readonly IUserManagementService _userManagementService;
    private readonly AuthorizationService _authorizationService;

    public ReadUserByLoginQueryHandler(
        IUserManagementService userManagementService,
        AuthorizationService authorizationService)
    {
        _userManagementService = userManagementService;
        _authorizationService = authorizationService;
    }
    
    public async Task<UserModel> Handle(ReadUserByLoginQuery query, CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsAdminUser(query.Principal, cancellationToken))
            throw new ForbiddenException();
        
        var user = await _userManagementService.GetUserByLogin(
            query.UserLogin,
            cancellationToken);

        if (user is null)
            throw new UserNotFoundException();
        
        return user;
    }
}