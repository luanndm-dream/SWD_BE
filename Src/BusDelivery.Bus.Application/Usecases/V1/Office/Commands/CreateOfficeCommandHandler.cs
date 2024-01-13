using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Office.Commands;
public class CreateOfficeCommandHandler : ICommandHandler<Command.CreateOfficeCommand, Responses.OfficeReponses>
{
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly OfficeRepository officeRepository;
    private readonly IMapper mapper;
    public CreateOfficeCommandHandler(IBlobStorageRepository blobStorageRepository, OfficeRepository officeRepository, IMapper mapper)
    {
        this.blobStorageRepository = blobStorageRepository;
        this.officeRepository = officeRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.OfficeReponses>> Handle(Command.CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = blobStorageRepository.SaveImageOnBlobStorage(request.image, request.name)
            ?? throw new Exception("Upload File fail");
        var office = new Domain.Entities.Office
        {
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
            officeRepository.Add(office);
        }
        catch (Exception)
        {
            blobStorageRepository.DeleteImageFromBlobStorage(imageUrl);
            throw new Exception("Create Office Error");
        }

        var officeResponse = mapper.Map<Responses.OfficeReponses>(office);
        return Result.Success(officeResponse, 201);
    }
}
