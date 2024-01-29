using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Reports;
using BusDelivery.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Application.Usecases.V1.Report.Commands;
public class CreateReportCommandHandler : ICommandHandler<Command.CreateReportCommandRequest, Responses.ReportResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateReportCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<Responses.ReportResponse>> Handle(Command.CreateReportCommandRequest request, CancellationToken cancellationToken)
    {
        var checkUser = await _context.User.AsNoTracking().Where(x => x.Id == request.CreateBy).SingleOrDefaultAsync();
        if (checkUser == null)
        {
            throw new Exception("UserID was not exist!");
        }
        else
        {
            try
            {
                var report = new Domain.Entities.Report()
                {
                    Content = request.Content,
                    CreateBy = request.CreateBy,
                    CreateTime = request.CreateTime,
                    TargetId = request.TargetId,
                    Type = request.Type,
                };
                await _context.AddAsync(report);
                var response = _mapper.Map<Responses.ReportResponse>(report);
                return Result.Success(response, 201);

            }
            catch
            {
                throw new Exception("Create new Report error!");
            }
        }
    }
}
