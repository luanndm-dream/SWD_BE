using BusDelivery.Contract.Abstractions.Shared;
using MediatR;

namespace BusDelivery.Contract.Abstractions.Message;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}

