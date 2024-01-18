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

namespace BusDelivery.Application.Usecases.V1.Bus.Queries;
public sealed class GetBusByIdHandler : IQueryHandler<Query.GetBusById, Responses.BusResponse>
{
    private readonly BusRepository busRepository;
    private readonly IMapper mapper;

    public GetBusByIdHandler(BusRepository busRepository, IMapper mapper)
    {
        this.busRepository = busRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.BusResponse>> Handle(Query.GetBusById request, CancellationToken cancellationToken)
    {
        var bus = await busRepository.FindByIdAsync(request.id)
            ?? throw new BusException.BusIdNotFoundException(request.id);
        var response = mapper.Map<Responses.BusResponse>(bus);
        return Result.Success(response);

    }
}
