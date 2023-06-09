using Aton.UserManagement.Api.Requests.V1;
using Aton.UserManagement.Api.Responses.V1;
using Aton.UserManagement.Bll.Commands;
using Aton.UserManagement.Bll.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aton.UserManagement.Api.Controllers.V1;

[ApiController]
[Route("/v1/create")]
public class CreateUserController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateUserController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<CreateResponse> Create(
        CreateUserRequest request,
        CancellationToken token)
    {
        var user = new UserModel(
            request.UserToCreate.Login,
            request.UserToCreate.Password,
            request.UserToCreate.Name,
            request.UserToCreate.Gender,
            request.UserToCreate.Birthday,
            request.UserToCreate.Admin,
            true
        );

        var command = new CreateUserCommand(
            new Principal(
                request.Principal.Login,
                request.Principal.Password
            ), user);

        var result = await _mediator.Send(command, token);

        return new CreateResponse(result);
    }
}