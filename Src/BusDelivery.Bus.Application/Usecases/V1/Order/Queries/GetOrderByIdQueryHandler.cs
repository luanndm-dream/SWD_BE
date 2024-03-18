using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Order;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Order.Queries;
public sealed class GetOrderByIdQueryHandler : IQueryHandler<Query.GetOrderByIdQuery, Responses.OrderResponses>
{
    private readonly OrderRepository orderRepository;
    private readonly IMapper mapper;
    private readonly IBlobStorageRepository blobStorageRepository;
    public GetOrderByIdQueryHandler(OrderRepository orderRepository, IMapper mapper, IBlobStorageRepository blobStorageRepository)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
    }
    public async Task<Result<Responses.OrderResponses>> Handle(Query.GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await orderRepository.FindByIdAsync(request.orderId)
    ?? throw new OrderException.OrderIdNotFoundException(request.orderId);

        //result.Image = await blobStorageRepository.GetImageToBase64(result.Image);

        var resultResponse = mapper.Map<Responses.OrderResponses>(result);

        return Result.Success(resultResponse);
    }
}
