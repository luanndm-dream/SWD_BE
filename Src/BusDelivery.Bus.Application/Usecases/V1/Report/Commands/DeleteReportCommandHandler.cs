using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Reports;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Application.Usecases.V1.Report.Commands;
public class DeleteReportCommandHandler : ICommandHandler<Command.DeleteReportCommandRequest>
{
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;

    public DeleteReportCommandHandler(ApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Result> Handle(Command.DeleteReportCommandRequest request, CancellationToken cancellationToken)
    {
        var report = await _context.Report.AsNoTracking().Where(x => x.Id == request.id).SingleOrDefaultAsync()
            ?? throw new ReportException.ReportIdNotFoundException(request.id);
        try
        {
            _context.RemoveRange(report);
            return Result.Success(202);
        }catch
        {
            throw new Exception("Delete Report error!");
        }
    }
}
