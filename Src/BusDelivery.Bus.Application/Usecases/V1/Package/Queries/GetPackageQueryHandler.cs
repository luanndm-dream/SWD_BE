using System.Globalization;
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

        if (request.status != null)
        {
            var statusPackage = request.status switch
            {
                1 => PackageStatus.Done,
                0 => PackageStatus.Processing,
                4 => PackageStatus.Delete,
                -1 => PackageStatus.Cancel,
            };

            EventsQuery = EventsQuery.Where(x => x.Status == statusPackage);
        }
        if (request.idOffice != null)
        {
            EventsQuery = EventsQuery.Where(x => x.ToOfficeId == request.idOffice);
        }
        if (request.toTime != null && request.fromTime != null)
        {
            DateTime fromTime = DateTime.ParseExact(request.fromTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime toTime = DateTime.ParseExact(request.toTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            EventsQuery = EventsQuery.Where(x => fromTime <= x.CreateTime && toTime >= x.CreateTime);
        }

        if (request.status == 4)
        {
            EventsQuery = EventsQuery.Where(x => x.Status == PackageStatus.Delete);
        }
        else
        {
            EventsQuery = EventsQuery.Where((x) => x.Status != PackageStatus.Delete);
        }

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.Package>.CreateAsync(EventsQuery,
        request.pageIndex,
        request.pageSize);

        foreach (var package in Events.items)
        {
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

