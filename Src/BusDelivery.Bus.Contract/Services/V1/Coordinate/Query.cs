using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Coordinate;
public class Query
{
    public record GetCoordinateQuery(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex, int pageSize)
        : IQuery<PagedResult<Responses.CoordinateResponses>>;

    public record GetCoordinateByIdQuery(int coordinateId) : IQuery<Responses.CoordinateResponses>;
}
