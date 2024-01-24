using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class RouteRepository : RepositoryBase<Route, int>
{
    public RouteRepository(ApplicationDbContext context) : base(context)
    {

    }
}

