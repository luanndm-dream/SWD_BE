using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class CoordinateRepository : RepositoryBase<Coordinate, int>
{
    public CoordinateRepository(ApplicationDbContext context) : base(context)
    {

    }
}
