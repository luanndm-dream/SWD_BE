using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Office.Commands;
public class UpdateOfficeCommandHandler : ICommandHandler<Command.UpdateOfficeCommand, Responses.OfficeReponses>
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

    public async Task<Result<Responses.OfficeReponses>> Handle(Command.UpdateOfficeCommand request, CancellationToken cancellationToken)
    {
        var existOffice = await officeRepository.FindByIdAsync(request.id)
            ?? throw new OfficeException.OfficeIdNotFoundException(request.id);

        // Delete oldImage and Upload newImage
        var oldimageUrl = existOffice.images;
        blobStorageRepository.DeleteImageFromBlobStorage(existOffice.images);
        var imageUrl = await blobStorageRepository.SaveImageOnBlobStorage(request.image, request.name, "offices")
            ?? throw new Exception("Upload File fail");

        existOffice.Update(
            request.id,
            request.routeId,
            request.name,
            request.address,
            request.lat,
            request.lng,
            request.contact,
            imageUrl,
            request.status);

        try
        {
            officeRepository.Update(existOffice);
            var officeResponse = mapper.Map<Responses.OfficeReponses>(existOffice);
            return Result.Success(officeResponse, 202);
        }
        catch (Exception)
        {
            await blobStorageRepository.RestoreContainer(oldimageUrl);
            await blobStorageRepository.DeleteImageFromBlobStorage(imageUrl);
            throw new Exception("Update Office Error");
        }
    }
}
