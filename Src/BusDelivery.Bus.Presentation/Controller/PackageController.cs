using Asp.Versioning;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Extensions;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusDelivery.Presentation.Controller;
[ApiVersion(1)]
[Authorize]
public class PackageController : ApiController
{
    public PackageController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.PackageResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Packages(
    int? idOffice,
    int? status,
    string? fromTime,
    string? toTime,
    string? sortOrder = null,
    int pageIndex = 1,
    int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetPackageQuery
            (idOffice,
            status,
            fromTime,
            toTime,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            pageIndex,
            pageSize));
        ;
        return Ok(result);
    }

    [HttpGet("{packageId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.PackageResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Packages([FromRoute] int packageId)
    {
        var result = await sender.Send(new Query.GetPackageByIdQuery(packageId));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.PackageResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Packages([FromForm] Command.CreatePackageCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPut("{packageId}")]
    [ProducesResponseType(typeof(Result<Responses.PackageResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Packages([FromRoute] int packageId, [FromForm] Command.UpdatePackageCommand request)
    {
        var updatePackage = new Command.UpdatePackageCommand
        (
            packageId,
            request.busId,
            request.fromOfficeId,
            request.toOfficeId,
            request.stationId,
            request.quantity,
            request.totalWeight,
            request.totalPrice,
            request.image,
            request.note,
            request.status
        );
        var result = await sender.Send(updatePackage);
        return Ok(result);
    }

    [HttpDelete("{packageId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePackages(int packageId)
    {
        var result = await sender.Send(new Command.DeletePackageCommand(packageId));
        return Ok(result);
    }
}
