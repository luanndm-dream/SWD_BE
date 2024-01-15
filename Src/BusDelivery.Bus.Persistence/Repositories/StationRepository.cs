using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class StationRepository : RepositoryBase<Station, int>
{
    public StationRepository(ApplicationDbContext context) : base(context)
    {
    }
}
