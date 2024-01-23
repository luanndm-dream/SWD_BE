using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class ReportRepository : RepositoryBase<Report, int>
{
    public ReportRepository(ApplicationDbContext context) : base(context)
    {
    }
}

