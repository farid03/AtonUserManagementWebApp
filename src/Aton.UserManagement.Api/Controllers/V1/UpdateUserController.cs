using Aton.UserManagement.Api.Requests.V1;
using Aton.UserManagement.Bll.Commands;
using Aton.UserManagement.Bll.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aton.UserManagement.Api.Controllers.V1;

[ApiController]
[Route("/v1/update")]
public class UpdateUserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateUserController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("info")]
    public async Task UpdateUserInfo(
        UpdateUserInfoRequest request,
        CancellationToken ct)
    {
        var query = new UpdateUserInfoCommand(
            new Principal(
                request.Principal.Login,
                request.Principal.Password
            ),
            request.UserLogin,
            request.Name,
            request.Gender,
            request.Birthday
        );

        await _mediator.Send(query, ct);
    }

    [HttpPatch("login")]
    public async Task UpdateUserLogin(
        UpdateUserLoginRequest request,
        CancellationToken ct)
    {
        var query = new UpdateUserLoginCommand(
            new Principal(
                request.Principal.Login,
                request.Principal.Password
            ),
            request.OldLogin,
            request.NewLogin
        );

        await _mediator.Send(query, ct);
    }

    [HttpPatch("password")]
    public async Task UpdateUserPassword(
        UpdateUserPasswordRequest request,
        CancellationToken ct)
    {
        var query = new UpdateUserPasswordCommand(
            new Principal(
                request.Principal.Login,
                request.Principal.Password
            ),
            request.UserLogin,
            request.NewPassword
        );

        await _mediator.Send(query, ct);
    }

    [HttpPatch("restore")]
    public async Task RestoreUser(
        RestoreUserRequest request,
        CancellationToken ct)
    {
        var query = new RestoreUserCommand(
            new Principal(
                request.Principal.Login,
                request.Principal.Password
            ),
            request.UserLogin
        );

        await _mediator.Send(query, ct);
    }
}