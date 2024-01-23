using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Bus;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Bus.Commands;
public sealed class UpdateBusCommandHandler : ICommandHandler<Command.UpdateBusCommandRequest, Responses.BusResponse>
{
    private readonly BusRepository busRepository;
    private readonly IMapper mapper;

    public UpdateBusCommandHandler(BusRepository busRepository, IMapper mapper)
    {
        this.busRepository = busRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.BusResponse>> Handle(Command.UpdateBusCommandRequest request, CancellationToken cancellationToken)
    {
        var bus = await busRepository.FindByIdAsync(request.id) 
            ?? throw new BusException.BusIdNotFoundException(request.id);
        try
        {
            bus.Number = request.number;
            bus.PlateNumber = request.number;
            bus.Name = request.name;
            bus.Organization = request.organization;
            bus.Color = request.color;
            bus.NumberOfSeat = request.numberOfSeat;
            bus.OperateTime = request.operateTime;
            bus.IsActive = request.IsActive;

            busRepository.Update(bus);
            var response = mapper.Map<Responses.BusResponse>(bus);
            return Result.Success(response);
        }
        catch
        {
            throw new Exception("Update bus error!");
        }

    }
}
