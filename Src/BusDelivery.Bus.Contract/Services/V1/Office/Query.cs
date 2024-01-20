using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Office;
public class Query
{
    public record GetOfficeQuery(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize)
        : IQuery<PagedResult<Responses.OfficeResponse>>;

    public record GetOfficeByIdQuery(int officeId) : IQuery<Responses.OfficeResponse>;
}
