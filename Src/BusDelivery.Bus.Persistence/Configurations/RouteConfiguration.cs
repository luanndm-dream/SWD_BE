using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable(TableName.Route);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Status).HasDefaultValue(true);
        builder.Property(x => x.StartPoint).IsRequired();
        builder.Property(x => x.EndPoint).IsRequired();
        builder.Property(x => x.OperateTime).IsRequired();

        //Each Route can have many Coordinates
        builder.HasMany(x => x.Coordinates)
            .WithOne()
            .HasForeignKey(r => r.RouteId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Route can have many BusRoutes
        builder.HasMany(x => x.BusRoutes)
            .WithOne()
            .HasForeignKey(r => r.RouteId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Route can have many StationRoutes
        builder.HasMany(x => x.StationRoutes)
            .WithOne()
            .HasForeignKey(r => r.RouteId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
