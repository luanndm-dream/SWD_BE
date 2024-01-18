using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Weather;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;
public class WeatherController : ApiController
{
    public WeatherController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Weathers()
    {
        var result = await sender.Send(new Command.Upsert());
        return Ok(result);
    }

    [HttpGet("GetWeather/{locationId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Weathers(int locationId)
    {
        var result = await sender.Send(new Query.GetWeatherQuery(locationId));
        if (result == null)
            HandlerFailure(result);
        return Ok(result);
    }
}
