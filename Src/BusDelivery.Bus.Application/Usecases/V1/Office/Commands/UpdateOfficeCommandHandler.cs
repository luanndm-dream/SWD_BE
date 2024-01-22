using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Office.Commands;
public sealed class UpdateOfficeCommandHandler : ICommandHandler<Command.UpdateOfficeCommand, Responses.OfficeResponse>
{
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly OfficeRepository officeRepository;
    private readonly IMapper mapper;
    public UpdateOfficeCommandHandler(IBlobStorageRepository blobStorageRepository, OfficeRepository officeRepository, IMapper mapper)
    {
        this.blobStorageRepository = blobStorageRepository;
        this.officeRepository = officeRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.OfficeResponse>> Handle(Command.UpdateOfficeCommand request, CancellationToken cancellationToken)
    {
        // Check existOffice
        var existOffice = await officeRepository.FindByIdAsync(request.id)
            ?? throw new OfficeException.OfficeIdNotFoundException(request.id);

        // Delete oldImage and Upload newImage
        var oldImageUrl = existOffice.Image;


        // Save NewImage and GetNewImageUrl
        var newImageUrl = await blobStorageRepository.SaveImageOnBlobStorage(request.image, request.name, "offices")
            ?? throw new Exception("Upload File fail");

        existOffice.Update(
            request.id,
            request.name,
            request.address,
            request.lat,
            request.lng,
            request.contact,
            newImageUrl,
            request.status);

        try
        {
            // update in Database
            officeRepository.Update(existOffice);
            // Map to Response
            var officeResponse = mapper.Map<Responses.OfficeResponse>(existOffice);
            // Delete oldImage In BlobStorage
            blobStorageRepository.DeleteImageFromBlobStorage(oldImageUrl);
            return Result.Success(officeResponse, 202);
        }
        catch (Exception)
        {
            await blobStorageRepository.DeleteImageFromBlobStorage(newImageUrl);
            throw new Exception("Update Office Error");
        }
    }
}
