using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Office.Commands;
public sealed class CreateOfficeCommandHandler : ICommandHandler<Command.CreateOfficeCommand, Responses.OfficeResponse>
{
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly OfficeRepository officeRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public CreateOfficeCommandHandler(
        IBlobStorageRepository blobStorageRepository,
        OfficeRepository officeRepository,
        IMapper mapper,
        ApplicationDbContext context)
    {
        this.blobStorageRepository = blobStorageRepository;
        this.officeRepository = officeRepository;
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<Result<Responses.OfficeResponse>> Handle(Command.CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = await blobStorageRepository.SaveImageOnBlobStorage(request.image, request.name, "offices")
            ?? throw new Exception("Upload File fail");
        var office = new Domain.Entities.Office
        {
            Name = request.name,
            Address = request.address,
            Lat = request.lat,
            Lng = request.lng,
            Contact = request.contact,
            Image = imageUrl,
            IsActive = request.status,
        };

        try
        {
            officeRepository.Add(office);
            await context.SaveChangesAsync();

            var officeResponse = mapper.Map<Responses.OfficeResponse>(office);
            return Result.Success(officeResponse, 201);
        }
        catch (Exception)
        {
            await blobStorageRepository.DeleteImageFromBlobStorage(imageUrl);
            throw new Exception("Create Office Error");
        }
    }
}
