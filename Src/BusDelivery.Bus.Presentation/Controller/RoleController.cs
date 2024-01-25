using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Role;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;
[ApiVersion(1)]
public class RoleController : ApiController
{
    public RoleController(ISender sender) : base(sender)
    {
    }

    [HttpGet("GetRoles")]
    [ProducesResponseType(typeof(Result<IReadOnlyCollection<Responses.RoleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Roles()
    {
        var result = await sender.Send(new Query.GetRoleQuery(null, null, null, 1, 10));
        return Ok(result);
    }

    [HttpGet("GetRoles/{RoleId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.RoleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Roles([FromRoute] int RoleId)
    {
        var result = await sender.Send(new Query.GetRoleByIdQuery(RoleId));
        return Ok(result);
    }


    [HttpPost("CreateRole")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Roles([FromForm] Command.CreateRoleCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [HttpPut("{RoleId}")]
    [ProducesResponseType(typeof(Result<Responses.RoleResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Roles([FromRoute] int RoleId, [FromForm] Command.UpdateRoleCommand request)
    {
        var updateRole = new Command.UpdateRoleCommand
        (
            RoleId,
            request.Name,
            request.Description
        );
        var result = await sender.Send(updateRole);
        return Ok(result);
    }


    [HttpDelete("{RoleId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRoles([FromRoute] int RoleId)
    {
        var result = await sender.Send(new Command.DeleteRoleCommand(RoleId));
        return Ok(result);
    }
}
