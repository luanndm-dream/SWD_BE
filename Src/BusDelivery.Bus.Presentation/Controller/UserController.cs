﻿using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Extensions;
using BusDelivery.Contract.Services.V1.User;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;
[ApiVersion(1)]
[Authorize]
public class UserController : ApiController
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [ProducesResponseType(typeof(Result<IReadOnlyCollection<Responses.UserResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Users(
        int? OfficeId = null,
        string? SearchTerm = null,
        string? SortColumn = null,
        string? SortOrder = null,
        int PageIndex = 1,
        int PageSize = 10)
    {
        var result = await sender.Send(new Query.GetUserQuery(
            OfficeId,                                                 // Filter Role
            SearchTerm,                                             // Value to search
            SortColumn,                                             // Column to sort
            SortOrderExtension.ConvertStringToSortOrder(SortOrder), // Get Asc or Des
            PageIndex,                                              // Page Value
            PageSize));
        return Ok(result);
    }

    [HttpGet("{UserId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.UserResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Users([FromRoute] int UserId)
    {
        var result = await sender.Send(new Query.GetUserByIdQuery(UserId));
        return Ok(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.UserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Users([FromForm] Command.CreateUserCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPut("{UserId}")]
    [ProducesResponseType(typeof(Result<Responses.UserResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Users([FromRoute] int UserId, [FromForm] Command.UpdateUserCommand request)
    {
        var updateUser = new Command.UpdateUserCommand
        (
            UserId,
            request.RoleId,
            request.OfficeId,
            request.Name,
            request.Email,
            request.PhoneNumber,
            request.Identity,
            request.Gentle,
            request.DeviceId,
            request.DeviceVersion,
            request.OS,
            request.IsActive,
            request.Avatar
        );
        var result = await sender.Send(updateUser);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{UserId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUsers([FromRoute] int UserId)
    {
        var result = await sender.Send(new Command.DeleteUserCommand(UserId));
        return Ok(result);
    }
}
