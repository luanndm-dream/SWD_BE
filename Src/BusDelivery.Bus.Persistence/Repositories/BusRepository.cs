using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Persistence.Repositories;
public class BusRepository : RepositoryBase<Bus, int>
{
    private readonly ApplicationDbContext context;
    public BusRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }
    public async Task<Bus?> CheckExistBusAsync(string plateNumber)
    {
        var bus = await context.Bus.Where(x => x.PlateNumber.Equals(plateNumber)).FirstOrDefaultAsync();
        return bus;
    }
}

