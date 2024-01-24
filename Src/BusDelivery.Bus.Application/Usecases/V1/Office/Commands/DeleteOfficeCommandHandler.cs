using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Office.Commands;
public sealed class DeleteOfficeCommandHandler : ICommandHandler<Command.DeleteOfficeCommand>
{
    private readonly OfficeRepository officeRepository;
    public DeleteOfficeCommandHandler(OfficeRepository officeRepository)
    {
        this.officeRepository = officeRepository;
    }
    public async Task<Result> Handle(Command.DeleteOfficeCommand request, CancellationToken cancellationToken)
    {
        var existOffice = await officeRepository.FindByIdAsync(request.id)
            ?? throw new OfficeException.OfficeIdNotFoundException(request.id);
        try
        {
            existOffice.IsActive = false;
            officeRepository.Update(existOffice);
            return Result.Success(202);
        }
        catch (Exception)
        {
            throw new Exception("Delete Office Error");
        }
    }
}
