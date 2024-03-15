using System.Globalization;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Package.Queries;
public sealed class GetPackageFromToHandler : IQueryHandler<Query.GetPackageFromTo, PagedResult<Responses.PackageResponse>>
{
    private readonly PackageRepository packageRepository;
    private readonly IMapper mapper;
    private readonly IBlobStorageRepository blobStorageRepository;
    public GetPackageFromToHandler(
        PackageRepository packageRepository,
        IMapper mapper, IBlobStorageRepository blobStorageRepository)
    {
        this.packageRepository = packageRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;

    }

    public async Task<Result<PagedResult<Responses.PackageResponse>>> Handle(Query.GetPackageFromTo request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.Package> EventsQuery;
        EventsQuery = packageRepository.FindAll();

        DateTime fromTime = DateTime.ParseExact(request.fromTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        DateTime toTime = DateTime.ParseExact(request.toTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        EventsQuery = EventsQuery.Where(x => fromTime <= x.CreateTime && toTime >= x.CreateTime);
        var Events = await PagedResult<Domain.Entities.Package>
            .CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        foreach (var package in Events.items)
        {
            package.Image = await blobStorageRepository.GetImageToBase64(package.Image);
        }
        var result = mapper.Map<PagedResult<Responses.PackageResponse>>(Events);
        return Result.Success(result);
    }
}
