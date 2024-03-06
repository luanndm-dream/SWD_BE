using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Package.Queries;
public sealed class GetPackageByIdOfficeHandle : IQueryHandler<Query.GetPackageByIdOffice, PagedResult<Responses.PackageResponse>>
{
    private readonly PackageRepository packageRepository;
    private readonly IMapper mapper;


    public GetPackageByIdOfficeHandle(
        PackageRepository packageRepository,
        IMapper mapper)
    {
        this.packageRepository = packageRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.PackageResponse>>> Handle(Query.GetPackageByIdOffice request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.Package> EventsQuery;
        EventsQuery = packageRepository.FindAll();
        var idOffice = request.idOffice;
        EventsQuery = EventsQuery.Where(x => x.ToOfficeId == idOffice);

        var Events = await PagedResult<Domain.Entities.Package>
        .CreateAsync(EventsQuery,
        request.pageIndex,
        request.pageSize);

        var result = mapper.Map<PagedResult<Responses.PackageResponse>>(Events);
        return Result.Success(result);
    }
}
