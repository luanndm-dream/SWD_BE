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
[Authorize]
public class OfficeController : ApiController
{
    public OfficeController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.OfficeResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Offices(
        bool? Status,
        string? SearchTerm = null,
        string? SortColumn = null,
        string? SortOrder = null,
        int? PageIndex = 1,
        int? PageSize = 10)
    {
        var result = await sender.Send(new Query.GetOfficeQuery(
            Status,
            SearchTerm,                                             // Value to search
            SortColumn,                                             // Column to sort
            SortOrderExtension.ConvertStringToSortOrder(SortOrder), // Get Asc or Des
            PageIndex,                                              // Page Value
            PageSize));                                             // PageSize
        return Ok(result);
    }

    [HttpGet("{officeId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.OfficeResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Offices([FromRoute] int officeId)
    {
        var result = await sender.Send(new Query.GetOfficeByIdQuery(officeId));
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
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
            request.Name,
            request.Address,
            request.Lat,
            request.Lng,
            request.Contact,
            request.OperationTime,
            request.Image,
            request.IsActive
        );
        var result = await sender.Send(updateOffice);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{OfficeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOffices(int OfficeId)
    {
        var result = await sender.Send(new Command.DeleteOfficeCommand(OfficeId));
        return Ok(result);
    }

}
