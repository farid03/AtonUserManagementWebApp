using Aton.UserManagement.Api.Responses.V1;
using Aton.UserManagement.Bll.Commands;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Queries;
using Aton.UserManagement.Api.Requests.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aton.UserManagement.Api.Controllers.V1;

[ApiController]
[Route("/v1/read")]
public class ReadUserController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReadUserController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("active")]
    public async Task<ReadAllActiveUsersResponse> ReadAllActiveUsers(
        ReadAllActiveUsersRequest request,
        CancellationToken ct)
    {
        var query = new ReadAllActiveUsersQuery(
            new Principal(
                request.Principal.Login,
                request.Principal.Password
            )
        );

        var users = await _mediator.Send(query, ct);

        return new ReadAllActiveUsersResponse(
            users
                .Select(x => new ReadAllActiveUsersResponse.User(
                    x.Name,
                    x.Gender,
                    x.Birthday,
                    x.Admin
                ))
                .ToArray()
        );
    }

    [HttpPost("login")]
    public async Task<ReadUserByLoginResponse> ReadUserByLogin(
        ReadUserByLoginRequest request,
        CancellationToken ct)
    {
        var query = new ReadUserByLoginQuery(
            new Principal(
                request.Principal.Login,
                request.Principal.Password
            ),
            request.UserLogin
        );

        var user = await _mediator.Send(query, ct);

        return new ReadUserByLoginResponse(
            user.Name,
            user.Gender,
            user.Birthday,
            user.IsActive
        );
    }

    [HttpPost("myself")]
    public async Task<ReadMyselfResponse> ReadMyself(
        ReadMyselfRequest request,
        CancellationToken ct)
    {
        var query = new ReadMyselfQuery(
            new Principal(
                request.Principal.Login,
                request.Principal.Password
            )
        );

        var user = await _mediator.Send(query, ct);

        return new ReadMyselfResponse(
            user.Name,
            user.Gender,
            user.Birthday,
            user.IsActive
        );
    }

    [HttpPost("older")]
    public async Task<ReadOlderThanResponse> ReadOlderThan(
        ReadOlderThanRequest request,
        CancellationToken ct)
    {
        var query = new ReadOlderThanQuery(
            new Principal(
                request.Principal.Login,
                request.Principal.Password
            ),
            request.Age
        );

        var users = await _mediator.Send(query, ct);

        return new ReadOlderThanResponse(
            users
                .Select(x => new ReadOlderThanResponse.User(
                    x.Name,
                    x.Gender,
                    x.Birthday,
                    x.Admin
                ))
                .ToArray()
        );
    }
}