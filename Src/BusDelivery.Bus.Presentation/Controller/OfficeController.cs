using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Extensions;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Presentation.Abstractions;
using MediatR;
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
    [ProducesResponseType(typeof(Result<PagedResult<Responses.OfficeReponses>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.OfficeReponses>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Offices(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetOfficeQuery(
            searchTerm,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            pageIndex,
            pageSize));
        return Ok(result);
    }


    [HttpGet("GetOffices/{officeId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.OfficeReponses>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Offices([FromRoute] int officeId)
    {
        var result = await sender.Send(new Query.GetOfficeByIdQuery(officeId));
        return Ok(result);
    }


    [HttpPost("CreateOffice")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Offices([FromForm] Command.CreateOfficeCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [HttpPut("{officeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Offices(int officeId, [FromForm] Command.UpdateOfficeCommand request)
    {
        var updateOffice = new Command.UpdateOfficeCommand
        (
            officeId,
            request.routeId,
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


    [HttpDelete("{officeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOffices(int officeId)
    {
        var result = await sender.Send(new Command.DeleteOfficeCommand(officeId));
        return Ok(result);
    }

}
