using Aton.UserManagement.Api.Requests.V1;
using Aton.UserManagement.Bll.Commands;
using Aton.UserManagement.Bll.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aton.UserManagement.Api.Controllers.V1;

[ApiController]
[Route("/v1/delete")]
public class DeleteUserController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteUserController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("soft")]
    public async Task SoftDelete(
        SoftDeleteRequest request,
        CancellationToken ct)
    {
        var query = new SoftDeleteUserCommand(
            new Principal(
                request.Principal.Login,
                request.Principal.Password
            ),
            request.UserLogin
        );

        await _mediator.Send(query, ct);
    }

    [HttpDelete("hard")]
    public async Task HardDelete(
        HardDeleteRequest request,
        CancellationToken ct)
    {
        var query = new HardDeleteUserCommand(
            new Principal(
                request.Principal.Login,
                request.Principal.Password
            ),
            request.UserLogin
        );

        await _mediator.Send(query, ct);
    }
}