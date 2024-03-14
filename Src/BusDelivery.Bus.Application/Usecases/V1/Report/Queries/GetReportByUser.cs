using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Reports;
using BusDelivery.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Application.Usecases.V1.Report.Queries;
public class GetReportByUser : IQueryHandler<Query.GetReportByUser, List<Responses.ReportResponse>>
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public GetReportByUser(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Result<List<Responses.ReportResponse>>> Handle(Query.GetReportByUser request, CancellationToken cancellationToken)
    {
        var checkUser = await context.User.AsNoTracking().Where(x => x.Id == request.userId).SingleOrDefaultAsync();

        if (checkUser == null)
        {
            throw new Exception("UserId was not exist!");
        }
        else
        {
            var report = await context.Report.AsNoTracking()
                .Where(x => x.CreateBy == request.userId)
                .Select(e => new Responses.ReportResponse()
                {
                    id = e.Id,
                    Content = e.Content,
                    CreateBy = e.CreateBy,
                    TargetId = e.TargetId,
                    Type = e.Type,
                    CreateTime = e.CreateTime.ToString(),
                })
                .ToListAsync()
                ?? throw new Exception("CreateBy was not exist");
            //var respone = new List<Responses.ReportResponse>();
            //foreach( var item in report)
            //{
            //    var 
            //}
            return await Task.FromResult(report);

        }
    }
}
