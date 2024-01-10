namespace BusDelivery.Domain.Abstractions.EntityBase;
public abstract class DomainEntity<T>
{
    public virtual T id { get; set; }

    public bool IsTransient()
        => id.Equals(default(T));
}
