using System.Linq.Expressions;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Package.Queries;
public sealed class GetPackageQueryHandler : IQueryHandler<Query.GetPackageQuery, PagedResult<Responses.PackageResponse>>
{
    private readonly PackageRepository packageRepository;
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly IMapper mapper;

    public GetPackageQueryHandler(
        PackageRepository packageRepository,
        IBlobStorageRepository blobStorageRepository,
        IMapper mapper)
    {
        this.packageRepository = packageRepository;
        this.blobStorageRepository = blobStorageRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.PackageResponse>>> Handle(Query.GetPackageQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.Package> EventsQuery;

        EventsQuery = packageRepository.FindAll();
        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.Package>.CreateAsync(EventsQuery,
        request.pageIndex,
        request.pageSize);

        foreach (var package in Events.items)
        {
            if (package.Image != "..")
                package.Image = await blobStorageRepository.GetImageToBase64(package.Image);
        }

        var result = mapper.Map<PagedResult<Responses.PackageResponse>>(Events);
        return Result.Success(result);
    }

    public static Expression<Func<Domain.Entities.Package, object>> GetSortProperty(Query.GetPackageQuery request)
    => request.sortOrder switch
    {
        _ => e => e.Id
    };

}

// Convert CreateTime, FromDay, ToDay sang DateTime
// FromDay< CreateTime < ToDay
