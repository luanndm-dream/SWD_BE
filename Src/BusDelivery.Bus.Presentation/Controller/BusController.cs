using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Extensions;
using BusDelivery.Contract.Services.V1.Bus;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;
[ApiVersion(1)]
[Authorize]
public class BusController : ApiController
{
    public BusController(ISender sender) : base(sender)
    {
    }
    [HttpGet("GetAllBus")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.BusResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.BusResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Bus(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetBus(
            searchTerm,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            pageIndex,
            pageSize));
        return Ok(result);
    }
    [HttpGet("GetBusById/{BusId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.BusResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBus([FromRoute] int BusId)
    {
        var result = await sender.Send(new Query.GetBusById(BusId));
        return Ok(result);
    }
    [HttpPost("CreateBus")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBus([FromForm] Command.CreateBusCommandRequest request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
    [HttpPut("UpdateStation")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBus([FromForm] Command.UpdateBusCommandRequest request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpDelete("DeleteBus")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBus([FromForm] Command.DeleteBusCommandRequest request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
}
