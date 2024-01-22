using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Persistence;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder builder)
    => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    public DbSet<Bus> Bus { get; set; }
    public DbSet<BusRoute> BusRoute { get; set; }
    public DbSet<Coordinate> Coordinate { get; set; }
    public DbSet<Office> Office { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Package> Package { get; set; }
    public DbSet<Report> Report { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Route> Route { get; set; }
    public DbSet<Station> Station { get; set; }
    public DbSet<StationRoute> StationRoute { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Weather> Weather { get; set; }
}
