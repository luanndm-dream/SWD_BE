using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable(TableName.Routes);
        builder.HasKey(x => x.id);

        builder.Property(x => x.name).IsRequired();
        builder.Property(x => x.description).IsRequired();
        builder.Property(x => x.status).HasDefaultValue(true);
        builder.Property(x => x.startPoint).IsRequired();
        builder.Property(x => x.endPoint).IsRequired();
        builder.Property(x => x.operateTime).IsRequired();

        //Each Route can have many Coordinates
        builder.HasMany(x => x.coordinates)
            .WithOne()
            .HasForeignKey(r => r.routeId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Route can have many BusRoutes
        builder.HasMany(x => x.busRoutes)
            .WithOne()
            .HasForeignKey(r => r.routeId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Route can have many StationRoutes
        builder.HasMany(x => x.stationRoutes)
            .WithOne()
            .HasForeignKey(r => r.routeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
