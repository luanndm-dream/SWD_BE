using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Bus;
using BusDelivery.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusDelivery.Application.Usecases.V1.Bus.Queries;
public sealed class GetBusHandler : IQueryHandler<Query.GetBus, PagedResult<Responses.BusResponse>>
{
    private readonly BusRepository _busRepository;
    private readonly IMapper _mapper;

    public GetBusHandler(BusRepository busRepository, IMapper mapper)
    {
        _busRepository = busRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.BusResponse>>> Handle(Query.GetBus request, CancellationToken cancellationToken)
    {
        var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
        ? _busRepository.FindAll()
        : _busRepository.FindAll(x => x.Name.Contains(request.searchTerm));

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.Bus>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        var result = _mapper.Map<PagedResult<Responses.BusResponse>>(Events);
        return Result.Success(result);
    }
    public static Expression<Func<Domain.Entities.Bus, object>> GetSortProperty(Query.GetBus request)
       => request.sortColumn?.ToLower() switch
       {
           "name" => e => e.Name,
           _ => e => e.Name
       };
}
