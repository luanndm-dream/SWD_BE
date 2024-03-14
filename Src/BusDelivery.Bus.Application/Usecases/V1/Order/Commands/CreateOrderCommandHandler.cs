using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Order;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Order.Commands;
public class CreateOrderCommandHandler : ICommandHandler<Command.CreateOrderCommand, Responses.OrderResponses>
{
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly OrderRepository orderRepository;
    private readonly IMapper mapper;

    public CreateOrderCommandHandler(IBlobStorageRepository blobStorageRepository, OrderRepository orderRepository, IMapper mapper)
    {
        this.blobStorageRepository = blobStorageRepository;
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.OrderResponses>> Handle(Command.CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = await blobStorageRepository.SaveImageOnBlobStorage(request.image, $"{request.contact}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}", "orders")
            ?? throw new Exception("Upload File fail");
        var order = new Domain.Entities.Order
        {
            PackageId = request.packageid,
            Weight = request.weight,
            Price = request.price,
            Note = request.note,
            Contact = request.contact,
            Image = imageUrl,
        };

        try
        {
            orderRepository.Add(order);
            var orderResponse = mapper.Map<Responses.OrderResponses>(order);
            return Result.Success(orderResponse, 201);
        }
        catch (Exception)
        {
            await blobStorageRepository.DeleteImageFromBlobStorage(imageUrl);
            throw new Exception("Create Order Error");
        }
    }
}
