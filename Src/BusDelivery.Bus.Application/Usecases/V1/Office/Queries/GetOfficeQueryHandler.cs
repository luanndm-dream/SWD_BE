using System.Linq.Expressions;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Office.Queries;
public sealed class GetOfficeQueryHandler : IQueryHandler<Query.GetOfficeQuery, PagedResult<Responses.OfficeReponses>>
{
    private readonly OfficeRepository officeRepository;
    private readonly IMapper mapper;

    public GetOfficeQueryHandler(OfficeRepository officeRepository, IMapper mapper)
    {
        this.officeRepository = officeRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.OfficeReponses>>> Handle(Query.GetOfficeQuery request, CancellationToken cancellationToken)
    {
        var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
        ? officeRepository.FindAll()
        : officeRepository.FindAll(x => x.Name.Contains(request.searchTerm) || x.Address.Contains(request.searchTerm));

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.Office>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        var result = mapper.Map<PagedResult<Responses.OfficeReponses>>(Events);
        return Result.Success(result);
    }

    // return e => e.property
    public static Expression<Func<Domain.Entities.Office, object>> GetSortProperty(Query.GetOfficeQuery request)
        => request.sortColumn?.ToLower() switch
        {
            "name" => e => e.Name,
            "address" => e => e.Address,
            _ => e => e.Name
        };
}
