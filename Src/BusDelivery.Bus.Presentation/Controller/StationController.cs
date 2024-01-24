using System;
using Asp.Versioning;
using Azure;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Extensions;
using BusDelivery.Contract.Services.V1.Station;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BusDelivery.Presentation.Controller;
[ApiVersion(1)]
public class StationController : ApiController
{
    public StationController(ISender sender) : base(sender)
    {
    }
    [HttpGet("GetStation")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.GetStationResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Stations(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetStation(
            searchTerm,                                             // Value to search
            sortColumn,                                             // Column to sort
            SortOrderExtension.ConvertStringToSortOrder(sortOrder), // Get Asc or Des
            pageIndex,                                              // Page Value
            pageSize));                                             // PageSize
        return Ok(result);
    }
    [HttpGet("GetStationById/{stationId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.GetStationResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Station([FromRoute] int stationId )
    {
        var result = await sender.Send(new Query.GetStationById(stationId));
        return Ok(result);
    }
    [HttpPost("CreateStation")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Station([FromForm] Command.CreateStationRequest request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
    [HttpPut("UpdateStation")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Station ([FromQuery] Command.UpdateStationRequest request)
    {
        //var updateStation = new Command.UpdateStationRequest(stationId)
        //{
        //    officeId = request.officeId,
        //    name = request.name,
        //    lat = request.lat,
        //    lng = request.lng,

        //};
        
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpDelete("DeleteStation/{stationId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOffices(int stationId)
    {
        var result = await sender.Send(new Command.DeleteStationRequest(stationId));
        return Ok(result);
    }
}
