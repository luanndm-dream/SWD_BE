using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static BusDelivery.Contract.Services.V1.Authentication.Responses;

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

    public async Task<IEnumerable<OrderChart>> GetChart()
    {

        int currentYear = DateTime.Now.Year;
        int lastYear = currentYear - 1;
        int month = DateTime.Now.Month;

        // Lấy dữ liệu từ tháng 3 năm trước đến tháng 3 năm hiện tại
        var monthlyQuantities = context.Package
            .Where(p => p.CreateTime.Year >= lastYear && p.CreateTime.Year <= currentYear &&
                        ((p.CreateTime.Year == lastYear && p.CreateTime.Month >= month) ||
                        (p.CreateTime.Year == currentYear && p.CreateTime.Month <= month)))
            .GroupBy(p => new { p.CreateTime.Month, p.CreateTime.Year })
            .Select(g => new OrderChart
            {
                Month = g.Key.Month,
                TotalOrder = g.Sum(p => p.Quantity)
            })
            .ToList();

        var result = Enumerable.Range(1, 12)
            .Select(month => new OrderChart
            {
                Month = month,
                TotalOrder = 0
            })
            .ToList();

        foreach (var monthlyQuantity in monthlyQuantities)
        {
            var index = monthlyQuantity.Month - 1;
            result[index].TotalOrder = monthlyQuantity.TotalOrder;
        }

        return result;

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
