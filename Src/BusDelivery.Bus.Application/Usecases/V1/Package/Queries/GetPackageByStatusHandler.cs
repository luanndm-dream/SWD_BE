using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Package.Queries;
public sealed class GetPackageByStatusHandler : IQueryHandler<Query.GetPackageByStatusQuery, PagedResult<Responses.PackageResponse>>
{
    private readonly PackageRepository packageRepository;
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly IMapper mapper;

    public GetPackageByStatusHandler(
        PackageRepository packageRepository,
        IMapper mapper,
        IBlobStorageRepository blobStorageRepository)
    {
        this.packageRepository = packageRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
    }

    public async Task<Result<PagedResult<Responses.PackageResponse>>> Handle(Query.GetPackageByStatusQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.Package> EventsQuery;
        EventsQuery = packageRepository.FindAll();
        var statusPackage = request.status switch
        {
            1 => PackageStatus.Done,
            0 => PackageStatus.Processing,
            _ => PackageStatus.Cancel,
        };

        EventsQuery = EventsQuery.Where(x => x.Status == statusPackage);

        var Events = await PagedResult<Domain.Entities.Package>
            .CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        //foreach (var package in Events.items)
        //{
        //    package.Image = await blobStorageRepository.GetImageToBase64(package.Image);
        //}

        var result = mapper.Map<PagedResult<Responses.PackageResponse>>(Events);
        return Result.Success(result);
    }
}

