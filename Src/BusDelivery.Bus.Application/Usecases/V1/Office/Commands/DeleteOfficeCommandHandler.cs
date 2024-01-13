﻿using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Office.Commands;
public class DeleteOfficeCommandHandler : ICommandHandler<Command.DeleteOfficeCommand>
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
        var imageUrl = existOffice.images;
        try
        {
            officeRepository.Remove(existOffice);

        }
        catch (Exception)
        {
            throw new Exception("Delete Office Error");
        }
        // Delete oldImage and Upload newImage
        blobStorageRepository.DeleteImageFromBlobStorage(imageUrl);
        return Result.Success(202);
    }
}
