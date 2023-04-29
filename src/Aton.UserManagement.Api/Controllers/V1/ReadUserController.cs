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

    [HttpPost]
    public async Task<CreateResponse> Get(
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}