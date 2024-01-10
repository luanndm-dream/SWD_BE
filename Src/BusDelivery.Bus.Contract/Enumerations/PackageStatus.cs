namespace BusDelivery.Contract.Enumerations;
public static class PackageStatus
{
    public const string Pending = nameof(Pending);
    public const string Shipped = nameof(Shipped);
    public const string Delivered = nameof(Delivered);
    public const string Cancelled = nameof(Cancelled);
}
