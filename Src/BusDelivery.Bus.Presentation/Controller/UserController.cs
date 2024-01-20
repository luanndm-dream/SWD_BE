using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Extensions;
using BusDelivery.Contract.Services.V1.User;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;
[ApiVersion(1)]
public class UserController : ApiController
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [HttpGet("GetUsers")]
    [ProducesResponseType(typeof(Result<IReadOnlyCollection<Responses.UserResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Users(
        Guid? roleId = null,
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetUserQuery(
            roleId,                                                 // Filter Role
            searchTerm,                                             // Value to search
            sortColumn,                                             // Column to sort
            SortOrderExtension.ConvertStringToSortOrder(sortOrder), // Get Asc or Des
            pageIndex,                                              // Page Value
            pageSize));
        return Ok(result);
    }

    [HttpGet("GetUsers/{UserId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.UserResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Users([FromRoute] Guid UserId)
    {
        var result = await sender.Send(new Query.GetUserByIdQuery(UserId));
        return Ok(result);
    }


    [HttpPost("CreateUser")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Users([FromForm] Command.CreateUserCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [HttpPut("{UserId}")]
    [ProducesResponseType(typeof(Result<Responses.UserResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Users([FromRoute] Guid UserId, [FromForm] Command.UpdateUserCommand request)
    {
        var updateUser = new Command.UpdateUserCommand
        (
            UserId,
            request.RoleId,
            request.OfficeId,
            request.Name,
            request.Email,
            request.HashPassword,
            request.PhoneNumber,
            request.Gentle,
            request.DeviceId,
            request.DeviceVersion,
            request.OS,
            request.CreateTime,
            request.IsDeleted,
            request.IsActive
        );
        var result = await sender.Send(updateUser);
        return Ok(result);
    }


    [HttpDelete("{UserId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUsers([FromRoute] Guid UserId)
    {
        var result = await sender.Send(new Command.DeleteUserCommand(UserId));
        return Ok(result);
    }
}
