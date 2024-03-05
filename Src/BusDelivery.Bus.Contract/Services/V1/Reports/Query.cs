using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Reports;
public class Query
{
    public record GetReport (string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex, int pageSize)
        : IQuery<PagedResult<Responses.ReportResponse>>;

    public record GetReportById(int reportId): IQuery<Responses.ReportResponse>;

    public record GetReportByUser(int userId) : IQuery<List<Responses.ReportResponse>>
    {
        // public Guid UserId { get; set; } 
    }
    public record CountReportByUserId( int userId) : IQuery<Responses.CountReport>;
}
