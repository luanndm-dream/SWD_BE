using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Persistence.Repositories;
public class PackageRepository : RepositoryBase<Package, int>
{
    private readonly ApplicationDbContext context;
    public PackageRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<int> TotalOrderInThisMonth()
    {
        var listPackageInThisMonth = await context.Package.
            Where(x => x.CreateTime.Month == DateTime.Now.Month)
            .ToListAsync();
        int total = 0;
        if (listPackageInThisMonth != null)
            foreach (var package in listPackageInThisMonth)
                total += package.Quantity;
        return total;
    }

    public async Task<float> RevenueInThisMonth()
    {
        var listPackageInThisMonth = await context.Package.
            Where(x => x.CreateTime.Month == DateTime.Now.Month)
            .ToListAsync();
        float total = 0;
        if (listPackageInThisMonth != null)
            foreach (var package in listPackageInThisMonth)
                total += package.TotalPrice;
        return total;
    }
}
