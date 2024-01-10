using BusDelivery.Domain.Abstractions;

namespace BusDelivery.Persistence;
public class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;

    public EFUnitOfWork(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync();
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
        => await context.DisposeAsync();
}

