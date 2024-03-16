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
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Weathers()
    {
        var result = await sender.Send(new Command.Upsert());
        return Ok(result);
    }

    [HttpGet("{OfficeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Weathers(int OfficeId)
    {
        var result = await sender.Send(new Query.GetWeatherQuery(OfficeId));
        if (result == null)
            HandlerFailure(result);
        return Ok(result);
    }
}
