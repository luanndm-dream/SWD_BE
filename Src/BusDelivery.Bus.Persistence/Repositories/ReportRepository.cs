using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Persistence.Repositories;
public class ReportRepository : RepositoryBase<Report, int>
{
    private readonly ApplicationDbContext context;
    public ReportRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<int> CountByUserId(int userId)
        => await context.Report.Where(x => x.CreateBy == userId).CountAsync();
}

