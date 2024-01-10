using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Persistence;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder builder)
    => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    public DbSet<Domain.Entities.Bus> Bus { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Domain.Entities.Path> Paths { get; set; }
}
