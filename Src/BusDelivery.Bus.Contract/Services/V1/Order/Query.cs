using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using static BusDelivery.Contract.Services.V1.Order.Responses;

namespace BusDelivery.Contract.Services.V1.Order;
public class Query
{
    public record GetOrderQuery(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize
        ) : IQuery<PagedResult<OrderResponses>>;

    public record GetOrderByIdQuery(Guid orderId) : IQuery<Responses.OrderResponses>;
}
