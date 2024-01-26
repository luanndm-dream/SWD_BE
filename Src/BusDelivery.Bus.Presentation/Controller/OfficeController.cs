using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Extensions;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;

[ApiVersion(1)]
public class OfficeController : ApiController
{
    public OfficeController(ISender sender) : base(sender)
    {
    }

    [HttpGet("GetOffices")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.OfficeResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Offices(
        bool? status,
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetOfficeQuery(
            status,
            searchTerm,                                             // Value to search
            sortColumn,                                             // Column to sort
            SortOrderExtension.ConvertStringToSortOrder(sortOrder), // Get Asc or Des
            pageIndex,                                              // Page Value
            pageSize));                                             // PageSize
        return Ok(result);
    }

    [HttpGet("GetOffices/{officeId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.OfficeResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Offices([FromRoute] int officeId)
    {
        var result = await sender.Send(new Query.GetOfficeByIdQuery(officeId));
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("CreateOffice")]
    [ProducesResponseType(typeof(Result<Responses.OfficeResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Offices([FromForm] Command.CreateOfficeCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{officeId}")]
    [ProducesResponseType(typeof(Result<Responses.OfficeResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Offices([FromRoute] int officeId, [FromForm] Command.UpdateOfficeCommand request)
    {
        var updateOffice = new Command.UpdateOfficeCommand
        (
            officeId,
            request.name,
            request.address,
            request.lat,
            request.lng,
            request.contact,
            request.image,
            request.status
        );
        var result = await sender.Send(updateOffice);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{officeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOffices(int officeId)
    {
        var result = await sender.Send(new Command.DeleteOfficeCommand(officeId));
        return Ok(result);
    }

}
