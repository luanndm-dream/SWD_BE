using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Extensions;
using BusDelivery.Contract.Services.V1.Order;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;
[ApiVersion(1)]
public class OrderController : ApiController
{
    public OrderController(ISender sender) : base(sender)
    {
    }
    [HttpGet("GetOrders")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.OrderResponses>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.OrderResponses>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Orders(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetOrderQuery(
            searchTerm,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            pageIndex,
            pageSize));
        return Ok(result);
    }

    [HttpGet("GetOrders/{orderId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.OrderResponses>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Orders([FromRoute] int orderId)
    {
        var result = await sender.Send(new Query.GetOrderByIdQuery(orderId));
        return Ok(result);
    }

    [HttpPost("CreateOrder")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Orders([FromForm] Command.CreateOrderCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPut("{orderId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Orders(int orderId, [FromForm] Command.UpdateOrderCommand request)
    {
        var updateOrder = new Command.UpdateOrderCommand
        (
            orderId,
            request.packageid,
            request.image,
            request.weight,
            request.price,
            request.note,
            request.contact);
        var result = await sender.Send(updateOrder);
        return Ok(result);
    }

    [HttpDelete("{orderId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        var result = await sender.Send(new Command.DeleteOrderCommand(orderId));
        return Ok(result);
    }
}
