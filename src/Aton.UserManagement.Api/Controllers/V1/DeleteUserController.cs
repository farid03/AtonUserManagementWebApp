using Aton.UserManagement.Api.Responses.V1;
using Aton.UserManagement.Bll.Commands;
using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Queries;
using Aton.UserManagement.Api.Requests.V1;
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
    
    [HttpPost]
    public async Task<CreateResponse> Delete(
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}