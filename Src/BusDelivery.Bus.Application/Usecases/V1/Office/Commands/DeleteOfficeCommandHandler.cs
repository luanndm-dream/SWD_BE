using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Office.Commands;
public sealed class DeleteOfficeCommandHandler : ICommandHandler<Command.DeleteOfficeCommand>
{
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly OfficeRepository officeRepository;
    public DeleteOfficeCommandHandler(IBlobStorageRepository blobStorageRepository, OfficeRepository officeRepository)
    {
        this.blobStorageRepository = blobStorageRepository;
        this.officeRepository = officeRepository;
    }
    public async Task<Result> Handle(Command.DeleteOfficeCommand request, CancellationToken cancellationToken)
    {
        var existOffice = await officeRepository.FindByIdAsync(request.id)
            ?? throw new OfficeException.OfficeIdNotFoundException(request.id);
        var imageUrl = existOffice.Image;
        try
        {
            officeRepository.Remove(existOffice);
            // Delete oldImage and Upload newImage
            await blobStorageRepository.DeleteImageFromBlobStorage(imageUrl);
            return Result.Success(202);
        }
        catch (Exception)
        {
            throw new Exception("Delete Office Error");
        }
    }
}
