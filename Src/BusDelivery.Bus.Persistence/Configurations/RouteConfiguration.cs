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
        builder.Property(x => x.officeId).IsRequired();
        builder.Property(x => x.busId).IsRequired();
        builder.Property(x => x.status).HasDefaultValue(true);

        //Each Bus can have many Routes
        builder.HasMany(x => x.paths)
            .WithOne()
            .HasForeignKey(r => r.routeId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Bus can have many BusRoutes
        builder.HasMany(x => x.busRoutes)
            .WithOne()
            .HasForeignKey(r => r.routeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
