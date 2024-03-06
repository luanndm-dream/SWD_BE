using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Order;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Order.Commands;
public sealed class UpdateOrderCommandHandler : ICommandHandler<Command.UpdateOrderCommand, Responses.OrderResponses>
{
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly OrderRepository orderRepository;
    private readonly IMapper mapper;

    public UpdateOrderCommandHandler(IBlobStorageRepository blobStorageRepository, OrderRepository orderRepository, IMapper mapper)
    {
        this.blobStorageRepository = blobStorageRepository;
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.OrderResponses>> Handle(Command.UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var existOrder = await orderRepository.FindByIdAsync(request.id.Value)
    ?? throw new OrderException.OrderIdNotFoundException(request.id.Value);

        var oldImageUrl = existOrder.Image;

        var newImageUrl = await blobStorageRepository.SaveImageOnBlobStorage(request.image, $"{request.contact}-{DateTimeOffset.Now.ToUnixTimeMilliseconds}", "orders")
        ?? throw new Exception("Upload File fail");

        existOrder.Update(
        request.id.Value,
        request.packageid,
        request.weight,
        request.price,
        request.note,
        request.contact,
        newImageUrl);

        try
        {
            // update in Database
            orderRepository.Update(existOrder);
            // Map to Response
            var orderResponse = mapper.Map<Responses.OrderResponses>(existOrder);
            // Delete oldImage In BlobStorage
            if (!string.IsNullOrEmpty(oldImageUrl))
                blobStorageRepository.DeleteImageFromBlobStorage(oldImageUrl);
            return Result.Success(orderResponse, 202);
        }
        catch (Exception)
        {
            await blobStorageRepository.DeleteImageFromBlobStorage(newImageUrl);
            throw new Exception("Update Order Error");
        }
    }
}
