using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Extensions;
using BusDelivery.Contract.Services.V1.Coordinate;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;
[ApiVersion(1)]
public class CoordinateController : ApiController
{
    public CoordinateController(ISender sender) : base(sender)
    {
    }

    [HttpGet("GetCoordinate")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.CoordinateResponses>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.CoordinateResponses>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Coordinates(
     string? searchTerm = null,
     string? sortColumn = null,
     string? sortOrder = null,
     int pageIndex = 1,
     int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetCoordinateQuery(
            searchTerm,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            pageIndex,
            pageSize));
        return Ok(result);
    }

    [HttpPost("CreateCoordinate")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Coordinates([FromForm] Command.CreateCoordinateCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpDelete("{CoordinateId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCoordinate(int CoordinateId)
    {
        var result = await sender.Send(new Command.DeleteCoordinateCommand(CoordinateId));
        return Ok(result);
    }


}
