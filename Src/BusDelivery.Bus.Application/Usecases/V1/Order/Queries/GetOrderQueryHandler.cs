using System.Linq.Expressions;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Order;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Order.Queries;
public sealed class GetOrderQueryHandler : IQueryHandler<Query.GetOrderQuery, PagedResult<Responses.OrderResponses>>
{
    private readonly OrderRepository orderRepository;
    private readonly IMapper mapper;
    public GetOrderQueryHandler(OrderRepository orderRepository, IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.OrderResponses>>> Handle(Query.GetOrderQuery request, CancellationToken cancellationToken)
    {
        var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
        ? orderRepository.FindAll()
        : orderRepository.FindAll(x => x.Weight.ToString().Contains(request.searchTerm) || x.Price.ToString().Contains(request.searchTerm));

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.Order>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        var result = mapper.Map<PagedResult<Responses.OrderResponses>>(Events);
        return Result.Success(result);
    }

    public static Expression<Func<Domain.Entities.Order, object>> GetSortProperty(Query.GetOrderQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "weight" => e => e.Weight,
        "price" => e => e.Price,
        _ => e => e.Price
    };
}
