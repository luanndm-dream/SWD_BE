using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Extensions;
using BusDelivery.Contract.Services.V1.Reports;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;

[ApiVersion(1)]
[Authorize]
public class ReportController : ApiController
{
    public ReportController(ISender sender) : base(sender)
    {
    }
    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ReportResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ReportResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Report(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetReport(
            searchTerm,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            pageIndex,
            pageSize));
        return Ok(result);
    }
    [HttpGet("ReportById/{reportId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ReportResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReportById([FromRoute] int reportId)
    {
        var result = await sender.Send(new Query.GetReportById(reportId));
        return Ok(result);
    }
    [HttpGet("ReportByUserID/{userId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ReportResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReport([FromRoute] int userId)
    {
        var result = await sender.Send(new Query.GetReportByUser(userId));
        return Ok(result);
    }

    [HttpGet("NumberOfReport/{userId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.CountReport>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CountReport([FromRoute] int userId)
    {
        var result = await sender.Send(new Query.CountReportByUserId(userId));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBus([FromForm] Command.CreateReportCommandRequest request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateReport([FromForm] Command.UpdateReportCommandRequest request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReport([FromForm] Command.DeleteReportCommandRequest request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

}
