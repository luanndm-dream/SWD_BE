using BusDelivery.Contract.Abstractions.Shared;
using MediatR;

namespace BusDelivery.Contract.Abstractions.Message;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
