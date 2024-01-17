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
    public DbSet<BusRoute> BusRoutes { get; set; }
    public DbSet<Coordinate> Coordinates { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Package> Packages { get; set; }
    //public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<StationRoute> StationRoutes { get; set; }
    public DbSet<User> Users { get; set; }
    //public DbSet<UserPackage> UserPackages { get; set; }
    //public DbSet<Weather> Weathers { get; set; }

}
