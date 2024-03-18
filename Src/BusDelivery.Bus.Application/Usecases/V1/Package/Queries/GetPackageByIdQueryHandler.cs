using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Package.Queries;
public sealed class GetPackageByIdQueryHandler : IQueryHandler<Query.GetPackageByIdQuery, Responses.PackageResponse>
{
    private readonly PackageRepository packageRepository;
    private readonly IMapper mapper;
    private readonly IBlobStorageRepository blobStorageRepository;


    public GetPackageByIdQueryHandler(
        PackageRepository packageRepository,
        IMapper mapper
        , IBlobStorageRepository blobStorageRepository)
    {
        this.packageRepository = packageRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
    }

    public async Task<Result<Responses.PackageResponse>> Handle(Query.GetPackageByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await packageRepository.FindByIdAsync(request.packageId)
            ?? throw new PackageException.PackageIdNotFoundException(request.packageId);

        result.Image = await blobStorageRepository.GetImageToBase64(result.Image);

        var resultResponse = mapper.Map<Responses.PackageResponse>(result);
        return Result.Success(resultResponse);
    }
}
