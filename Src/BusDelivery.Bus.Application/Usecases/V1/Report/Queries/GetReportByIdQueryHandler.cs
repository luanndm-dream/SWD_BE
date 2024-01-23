using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Reports;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Application.Usecases.V1.Report.Queries;
public class GetReportByIdQueryHandler : IQueryHandler<Query.GetReportById, Responses.ReportResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetReportByIdQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<Responses.ReportResponse>> Handle(Query.GetReportById request, CancellationToken cancellationToken)
    {
        var report = await _dbContext.Report.AsNoTracking().Where(x => x.Id == request.id).SingleOrDefaultAsync() 
            ?? throw new ReportException.ReportIdNotFoundException(request.id);
        var response = _mapper.Map<Responses.ReportResponse>(report);
        return Result.Success(response);
    }
}
