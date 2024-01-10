using BusDelivery.Contract.Abstractions.Shared;
using MediatR;

namespace BusDelivery.Contract.Abstractions.Message;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
