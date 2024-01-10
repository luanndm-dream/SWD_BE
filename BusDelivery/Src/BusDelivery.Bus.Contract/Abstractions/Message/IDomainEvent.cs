using MediatR;

namespace BusDelivery.Contract.Abstractions.Message;
public interface IDomainEvent : INotification
{
    public Guid Id { get; init; } // init khoi tao 1 lan ban dau
}
