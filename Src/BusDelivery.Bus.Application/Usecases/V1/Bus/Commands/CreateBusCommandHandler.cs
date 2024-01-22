using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Bus;
using BusDelivery.Persistence;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Bus.Commands;
public sealed class CreateBusCommandHandler : ICommandHandler<Command.CreateBusCommandRequest, Responses.BusResponse>
{
    private readonly BusRepository busRepository;
    private readonly IMapper mapper;

    public CreateBusCommandHandler(BusRepository busRepository, IMapper mapper)
    {
        this.busRepository = busRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.BusResponse>> Handle(Command.CreateBusCommandRequest request, CancellationToken cancellationToken)
    {
        var bus = await busRepository.CheckExistBusAsync(request.plateNumber);
        if (bus != null)
        {
            throw new Exception("This plateNumber was registered!");
        }
        else
        {
            var newBus = new Domain.Entities.Bus()
            {
                Number = request.number,
                PlateNumber = request.plateNumber,
                Name = request.name,
                Organization = request.organization,
                Color = request.color,
                NumberOfSeat = request.numberOfSeat,
                OperateTime = request.operateTime,
                IsActive = request.IsActive
            };
            try
            {
                busRepository.Add(newBus);
                var response = mapper.Map<Responses.BusResponse>(newBus);
                return Result.Success(response, 201);
            }
            catch
            {
                throw new Exception("Add new Bus error!");
            }
        }

    }
}
