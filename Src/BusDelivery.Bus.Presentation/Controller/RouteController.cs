using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Extensions;
using BusDelivery.Contract.Services.V1.Route;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;
public class RouteController : ApiController
{
    public RouteController(ISender sender) : base(sender)
    {
    }
    [HttpGet("GetAllRoute")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.RouteResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.RouteResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Bus(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetRoute(
            searchTerm,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            pageIndex,
            pageSize));
        return Ok(result);
    }
    [HttpGet("GetRouteById/{routeId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.RouteResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoute([FromRoute] int id)
    {
        var result = await sender.Send(new Query.GetRouteById(id));
        return Ok(result);
    }
    [HttpPost("CreateRoute")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRoute([FromForm] Command.CreateRouteCommandRequest request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
    [HttpPut("UpdateRoute")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStation([FromQuery] Command.UpdateRouteCommandRequest request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpDelete("DeleteRoute/{routeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deleteroute(int id)
    {
        var result = await sender.Send(new Command.DeleteRouteCommandRequest(id));
        return Ok(result);
    }
}

