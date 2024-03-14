using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Package.Commands;
public sealed class UpdatePackageCommandHandler : ICommandHandler<Command.UpdatePackageCommand, Responses.PackageResponse>
{
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly PackageRepository packageRepository;
    private readonly IMapper mapper;
    public UpdatePackageCommandHandler(IBlobStorageRepository blobStorageRepository, PackageRepository packageRepository, IMapper mapper)
    {
        this.blobStorageRepository = blobStorageRepository;
        this.packageRepository = packageRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.PackageResponse>> Handle(Command.UpdatePackageCommand request, CancellationToken cancellationToken)
    {
        var existPackage = await packageRepository.FindByIdAsync(request.id.Value)
        ?? throw new PackageException.PackageIdNotFoundException(request.id.Value);

        var oldImageUrl = existPackage.Image;


        var newImageUrl = await blobStorageRepository.SaveImageOnBlobStorage(request.image, $"{request.fromOfficeId}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}", "packages")

        ?? throw new Exception("Upload File fail");

        existPackage.Update(
            request.id.Value,
            request.busId,
            request.fromOfficeId,
            request.toOfficeId,
            request.stationId,
            request.quantity,
            request.totalWeight,
            request.totalPrice,
            newImageUrl,
            request.note,
            request.status);
        try
        {
            // update in Database
            packageRepository.Update(existPackage);
            // Map to Response
            var officeResponse = mapper.Map<Responses.PackageResponse>(existPackage);
            // Delete oldImage In BlobStorage
            if (string.IsNullOrEmpty(oldImageUrl))
                blobStorageRepository.DeleteImageFromBlobStorage(oldImageUrl);

            return Result.Success(officeResponse, 202);
        }
        catch (Exception)
        {
            await blobStorageRepository.DeleteImageFromBlobStorage(newImageUrl);
            throw new Exception("Update Package Error");
        }
    }

}
