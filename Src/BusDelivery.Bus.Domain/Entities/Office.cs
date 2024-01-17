using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Office : DomainEntity<int>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Lat { get; set; }
    public string Lng { get; set; }
    public string Contact { get; set; }
    public string Image { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Station> Stations { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Package> Packages { get; set; }

    public void Update(
        int id,
        string name,
        string address,
        string lat,
        string lng,
        string contact,
        string image,
        bool status)
    {
        Id = id;
        Name = name;
        Address = address;
        Lat = lat;
        Lng = lng;
        Contact = contact;
        Image = image;
        IsActive = status;
    }
}
