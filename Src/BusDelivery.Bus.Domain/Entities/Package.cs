using BusDelivery.Contract.Enumerations;
using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Package : DomainEntity<int>
{
    public int BusId { get; set; }
    public int FromOfficeId { get; set; }
    public int ToOfficeId { get; set; }
    public int StationId { get; set; }
    public int Quantity { get; set; }
    public float TotalWeight { get; set; }
    public float TotalPrice { get; set; }
    public string Image { get; set; }
    public string Note { get; set; }
    public PackageStatus Status { get; set; }
    public DateTime CreateTime { get; set; }

    public virtual ICollection<Order> Orders { get; set; }

    public void Update(
    int id,
    int busId,
    int fromOfficeId,
    int toOfficeId,
    int stationId,
    int quantity,
    float totalWeight,
    float totalPrice,
    string image,
    string note,
    PackageStatus status)
    {
        Id = id;
        BusId = busId;
        FromOfficeId = fromOfficeId;
        ToOfficeId = toOfficeId;
        StationId = stationId;
        Quantity = quantity;
        TotalWeight = totalWeight;
        TotalPrice = totalPrice;
        Image = image;
        Note = note;
        Status = status;
    }
}
