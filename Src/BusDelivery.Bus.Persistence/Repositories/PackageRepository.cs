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

    public async Task<int> TotalOrder()
        => await context.Package.SumAsync(x => x.Quantity);


    public async Task<float> TotalPriceThisMonth()
    {
        var listPackageInThisMonth = await context.Package.
            Where(x => x.CreateTime.Month == DateTime.Now.Month
            && x.CreateTime.Year == DateTime.Now.Year)
            .ToListAsync();
        return listPackageInThisMonth?.Sum(x => x.TotalPrice) ?? 0;
    }

    public async Task<int> NewOrderInThisMonth()
    {
        var listPackageInThisMonth = await context.Package.
            Where(x => x.CreateTime.Month == DateTime.Now.Month
            && x.CreateTime.Year == DateTime.Now.Year)
            .ToListAsync();

        return listPackageInThisMonth?.Sum(x => x.Quantity) ?? 0;
    }

    public async Task<int> TotalOrderLastMonth()
    {
        var day = GetFirstDayOfPCurrentMonth();

        var listPackageLastMonth = await context.Package.
            Where(x => x.CreateTime < day)
            .ToListAsync();
        return listPackageLastMonth?.Sum(package => package.Quantity) ?? 0;
    }

    private DateTime GetFirstDayOfPCurrentMonth()
        => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
}
