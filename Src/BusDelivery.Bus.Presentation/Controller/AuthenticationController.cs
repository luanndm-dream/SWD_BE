﻿using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Authentication;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;
[ApiVersion(1)]
public class AuthenticationController : ApiController
{
    public AuthenticationController(ISender sender) : base(sender)
    {
    }

    [HttpPost("Register")]
    [ProducesResponseType(typeof(Result<Responses.UserReponses>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromForm] Command.RegisterCommand request)
    {
        var result = await sender.Send(request);
        if (!result.IsSuccess)
            HandlerFailure(result);

        return Ok(result);
    }

    [HttpPost("LoginAsync")]
    [ProducesResponseType(typeof(Result<Responses.LoginResponses>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LoginAsync(Command.LoginCommand loginRequest)
    {
        var result = await sender.Send(loginRequest);

        if (!result.IsSuccess)
            HandlerFailure(result);

        return Ok(result);
    }
}
