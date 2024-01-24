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
using BusDelivery.Contract.Services.V1.Reports;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Report.Queries;
public class GetReportQueryHandler : IQueryHandler<Query.GetReport, PagedResult<Responses.ReportResponse>>
{
    private readonly ReportRepository reportRepository;
    private readonly IMapper mapper;

    public GetReportQueryHandler(ReportRepository reportRepository, IMapper mapper)
    {
        this.reportRepository = reportRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.ReportResponse>>> Handle(Query.GetReport request, CancellationToken cancellationToken)
    {
        // Check value search is nullOrWhiteSpace?
        var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
        ? reportRepository.FindAll()   // If Null GetAll
        : reportRepository.FindAll(x => x.Content.Contains(request.searchTerm) || x.TargetId.ToString().Contains(request.searchTerm)); // If Not GetAll With Name Or Address Contain searchTerm

        // Get Func<TEntity,TResponse> column
        var keySelector = GetSortProperty(request);

        // Asc Or Des
        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        // GetList by Pagination
        var Events = await PagedResult<Domain.Entities.Report>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        var result = mapper.Map<PagedResult<Responses.ReportResponse>>(Events);
        return Result.Success(result);
    }

    // return e => e.property
    public static Expression<Func<Domain.Entities.Report, object>> GetSortProperty(Query.GetReport request)
        => request.sortColumn?.ToLower() switch
        {
            "Name" => e => e.Content,
            "TargetId" => e => e.TargetId,
            _ => e => e.Content
        };
}

