using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Order;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Order.Commands;
public sealed class DeleteOrderCommandHandler : ICommandHandler<Command.DeleteOrderCommand>
{
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly OrderRepository orderRepository;
    public DeleteOrderCommandHandler(IBlobStorageRepository blobStorageRepository, OrderRepository orderRepository)
    {
        this.blobStorageRepository = blobStorageRepository;
        this.orderRepository = orderRepository;
    }
    public async Task<Result> Handle(Command.DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var existOrder = await orderRepository.FindByIdAsync(request.id)
        ?? throw new OrderException.OrderIdNotFoundException(request.id);

        var imageUrl = existOrder.Image;
        try
        {
            orderRepository.Remove(existOrder);
            // Delete oldImage and Upload newImage
            await blobStorageRepository.DeleteImageFromBlobStorage(imageUrl);
            return Result.Success(202);
        }
        catch (Exception)
        {
            throw new Exception("Delete Order Error");
        }
    }
}

