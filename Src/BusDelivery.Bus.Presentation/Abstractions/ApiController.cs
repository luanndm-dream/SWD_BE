using BusDelivery.Contract.Abstractions.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Abstractions;
[ApiController]
[Route("Api/V{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender sender;

    protected ApiController(ISender sender) => this.sender = sender;

    protected IActionResult HandlerFailure(Result result)
        => result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            _ => BadRequest(result)
        };
}

