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
        blobStorageRepository.DeleteImageFromBlobStorage(existOffice.images);
        var imageUrl = blobStorageRepository.SaveImageOnBlobStorage(request.image, request.name)
            ?? throw new Exception("Upload File fail");

        var updateOffice = new Domain.Entities.Office
        {
            id = request.id,
            routeId = request.routeId,
            name = request.name,
            address = request.address,
            lat = request.lat,
            lng = request.lng,
            contact = request.contact,
            images = imageUrl,
            status = request.status,
        };

        try
        {
            officeRepository.Update(updateOffice);

        }
        catch (Exception)
        {
            blobStorageRepository.DeleteImageFromBlobStorage(imageUrl);
            throw new Exception("Update Office Error");
        }

        var officeResponse = mapper.Map<Responses.OfficeReponses>(updateOffice);
        return Result.Success(officeResponse, 202);
    }
}
