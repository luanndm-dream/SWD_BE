using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Reports;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Application.Usecases.V1.Report.Commands;
public class UpdateReportCommandHandler : ICommandHandler<Command.UpdateReportCommandRequest, Responses.ReportResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateReportCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<Responses.ReportResponse>> Handle(Command.UpdateReportCommandRequest request, CancellationToken cancellationToken)
    {
        var report = await _context.Report.AsNoTracking().Where(x => x.Id == request.id).SingleOrDefaultAsync()
            ?? throw new ReportException.ReportIdNotFoundException(request.id);
        try
        {
            var updateReport = _mapper.Map<Domain.Entities.Report>(report);

            updateReport.Content = request.Content;
            updateReport.TargetId = request.TargetId;
            updateReport.CreateBy = request.CreateBy;
            updateReport.CreateTime = request.CreateTime;
            updateReport.Type = request.Type;

            _context.Update(updateReport);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<Responses.ReportResponse>(updateReport);
            return Result.Success(response);
        }
        catch
        {
            throw new Exception("Update Report error!");
        }
    }
}
