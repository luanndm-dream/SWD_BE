using System;
using Asp.Versioning;
using Azure;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Station;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;
[ApiVersion(1)]
public class StationController : ApiController
{
    public StationController(ISender sender) : base(sender)
    {
    }
    [HttpGet("GetStationById/{stationId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.GetStationResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Station([FromRoute] int stationId )
    {
        var result = await sender.Send(new Query.GetStationById(stationId));
        return Ok(result);
    }
}
