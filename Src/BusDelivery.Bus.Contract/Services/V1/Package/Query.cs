using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Package;
public class Query
{
    public record GetPackageQuery(
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize)
    : IQuery<PagedResult<Responses.PackageResponse>>;

    public record GetPackageFromTo(string fromTime, string toTime, int pageIndex, int pageSize) : IQuery<PagedResult<Responses.PackageResponse>>;
    public record GetPackageByIdQuery(int packageId) : IQuery<Responses.PackageResponse>;
    public record GetPackageByIdOffice(int idOffice, int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.PackageResponse>>;
    public record GetPackageByStatusQuery(int status, int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.PackageResponse>>;



}
