using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Bus;
using BusDelivery.Domain.Entities;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Bus.Commands;
public sealed class DeleteBusCommandHandler : ICommandHandler<Command.DeleteBusCommandRequest>
{
    private readonly BusRepository busRepository;

    public DeleteBusCommandHandler(BusRepository busRepository)
    {
        this.busRepository = busRepository;
    }

    public async Task<Result> Handle(Command.DeleteBusCommandRequest request, CancellationToken cancellationToken)
    {
        var bus = await busRepository.FindByIdAsync(request.id)
            ?? throw new BusException.BusIdNotFoundException(request.id);

        try
        {
            bus.IsActive = false;
            busRepository.Update(bus);
            return Result.Success(202);
        }
        catch
        {
            throw new Exception("Delete Bus not success!");
        }
    }
}
