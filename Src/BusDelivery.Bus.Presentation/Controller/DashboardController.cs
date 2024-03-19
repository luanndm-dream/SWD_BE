using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Dashboard;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;

[ApiVersion(1)]
[Authorize(Roles = "Admin")]
public class DashboardController : ApiController
{
    public DashboardController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<Responses.DashboardResponses>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDashboard()
    {
        var result = await sender.Send(new Query.GetDashboardQuery());

        return Ok(result);
    }
}
