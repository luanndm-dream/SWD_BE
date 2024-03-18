using System.Linq.Expressions;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Order;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Order.Queries;
public sealed class GetOrderQueryHandler : IQueryHandler<Query.GetOrderQuery, PagedResult<Responses.OrderResponses>>
{
    private readonly OrderRepository orderRepository;
    private readonly IMapper mapper;
    private readonly IBlobStorageRepository blobStorageRepository;
    public GetOrderQueryHandler(OrderRepository orderRepository, IMapper mapper, IBlobStorageRepository blobStorageRepository)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
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

        //foreach (var oder in Events.items)
        //{
        //    oder.Image = await blobStorageRepository.GetImageToBase64(oder.Image);
        //}
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
