using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Package;
public class CreatePackageCommandHandler : ICommandHandler<Command.CreatePackageCommand, Responses.PackageResponse>
{
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly PackageRepository packageRepository;
    private readonly IMapper mapper;
    public CreatePackageCommandHandler(IBlobStorageRepository blobStorageRepository, PackageRepository packageRepository, IMapper mapper)
    {
        this.blobStorageRepository = blobStorageRepository;
        this.packageRepository = packageRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.PackageResponse>> Handle(Command.CreatePackageCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = await blobStorageRepository.SaveImageOnBlobStorage(request.image, $"{request.note}-{DateTimeOffset.Now.ToUnixTimeMilliseconds}", "package")
    ?? throw new Exception("Upload File fail");
        var packeage = new Domain.Entities.Package
        {
            BusId = request.busId,
            OfficeId = request.officeId,
            StationId = request.stationId,
            Quantity = request.quantity,
            TotalWeight = request.totalWeight,
            TotalPrice = request.totalPrice,
            Image = imageUrl,
            Note = request.note,
            Status = request.status,
            CreateTime = request.createTime,

        };
    }
}
