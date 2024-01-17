namespace BusDelivery.Domain.Abstractions.EntityBase;
public abstract class DomainEntity<T>
{
    public virtual T Id { get; set; }

    public bool IsTransient()
        => Id.Equals(default(T));
}
