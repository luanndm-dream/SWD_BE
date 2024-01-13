﻿using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Office : DomainEntity<int>
{
    public int routeId { get; set; }
    public string name { get; set; }
    public string address { get; set; }
    public string lat { get; set; }
    public string lng { get; set; }
    public string contact { get; set; }
    public string images { get; set; }
    public bool status { get; set; }

    public virtual ICollection<Weather> weathers { get; set; }
    public virtual ICollection<Station> stations { get; set; }
    public virtual ICollection<User> users { get; set; }
    public virtual ICollection<Package> packages { get; set; }
}
