using Aton.UserManagement.Api.Requests.V1;
using Aton.UserManagement.Api.Responses.V1;
using Aton.UserManagement.Bll.Commands;
using Aton.UserManagement.Bll.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aton.UserManagement.Api.Controllers.V1;
// TODO добавить мидлвейр авторизации
// TODO добавить мидлвейр хеширования паролей
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
            request.UserToCreate.Password, // TODO не забыть хешировать
            request.UserToCreate.Name,
            request.UserToCreate.Gender,
            request.UserToCreate.Birthday,
            request.UserToCreate.Admin
        );
        
        var command = new CreateUserCommand(request.Login, user);
            
        var result = await _mediator.Send(command, token);

        return new CreateResponse(result);
    }
}