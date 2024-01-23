using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Package;
public class Query
{
    public record GetPackageQuery(
    string? searchTerm,
    string? sortColumn,
    SortOrder? sortOrder,
    int pageIndex,
    int pageSize)
    : IQuery<PagedResult<Responses.PackageResponse>>;

    public record GetPackageByIdQuery(Guid packageId) : IQuery<Responses.PackageResponse>;
    public record GetPackageByDateQuery(string date) : IQuery<Responses.PackageResponse>;
    public record GetPackageByStatusQuery(bool status) : IQuery<Responses.PackageResponse>;



}
