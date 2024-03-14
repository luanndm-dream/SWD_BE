using System.Linq.Expressions;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Office.Queries;
public sealed class GetOfficeQueryHandler : IQueryHandler<Query.GetOfficeQuery, PagedResult<Responses.OfficeResponse>>
{
    private readonly OfficeRepository officeRepository;
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly IMapper mapper;

    public GetOfficeQueryHandler(
        OfficeRepository officeRepository,
        IBlobStorageRepository blobStorageRepository,
        IMapper mapper)
    {
        this.officeRepository = officeRepository;
        this.blobStorageRepository = blobStorageRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.OfficeResponse>>> Handle(Query.GetOfficeQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.Office> EventsQuery;
        if (string.IsNullOrWhiteSpace(request.searchTerm))
        {
            EventsQuery = (request.isActive == null)
                ? officeRepository.FindAll()
                : officeRepository.FindAll(x => x.IsActive == request.isActive);
        }
        else
        {
            EventsQuery = (request.isActive == null)
                ? officeRepository.FindAll(x => x.Name.Contains(request.searchTerm) || x.Address.Contains(request.searchTerm))
                : officeRepository.FindAll(x => x.IsActive == request.isActive && (x.Name.Contains(request.searchTerm) || x.Address.Contains(request.searchTerm)));
        }

        // Get Func<TEntity,TResponse> column
        var keySelector = GetSortProperty(request);

        // Asc Or Des
        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        // GetList by Pagination
        var Events = await PagedResult<Domain.Entities.Office>.CreateAsync(EventsQuery,
            request.pageIndex.Value,
            request.pageSize.Value);

        // Encode toBase64String
        foreach (var office in Events.items)
        {
            office.Image = await blobStorageRepository.GetImageToBase64(office.Image);
        }

        var result = mapper.Map<PagedResult<Responses.OfficeResponse>>(Events);
        return Result.Success(result);
    }

    // return e => e.property
    public static Expression<Func<Domain.Entities.Office, object>> GetSortProperty(Query.GetOfficeQuery request)
        => request.sortColumn?.ToLower() switch
        {
            "Name" => e => e.Name,
            "Address" => e => e.Address,
            _ => e => e.Name
        };
}
