using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Reports;
using BusDelivery.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Application.Usecases.V1.Report.Queries;
public class CountReportByUser : IQueryHandler<Query.CountReportByUserId, Responses.CountReport>
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CountReportByUser(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.CountReport>> Handle(Query.CountReportByUserId request, CancellationToken cancellationToken)
    {
        var checkUser = await context.User.AsNoTracking().Where(x => x.Id == request.userId).FirstOrDefaultAsync();
        if (checkUser == null)
        {
            throw new Exception("UserId was not exist!");
        }
        else
        {
            var reportCount = await context.Report.AsNoTracking().CountAsync(x => x.CreateBy == request.userId);

            var countReport = new Responses.CountReport()
            {
                number = reportCount // Adjust based on your actual model
            };

            // Create a Result<Responses.CountReport> with the countReport instance
            var result = Result<Responses.CountReport>.Success(countReport);

            return result;
        }
    }
}
