using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Order : DomainEntity<int>
{
    public int PackageId { get; set; }
    public string Image { get; set; }
    public float Weight { get; set; }
    public float Price { get; set; }
    public string Note { get; set; }
    public string Contact { get; set; }

    public void Update(
    Guid id,
    Guid packageId,
    float weight,
    float price,
    string note,
    string contact,
    string image)
    {
        Id = id;
        PackageId = packageId;
        Weight = weight;
        Price = price;
        Note = note;
        Contact = contact;
        Image = image;
    }
}
