using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class StationConfiguration : IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.ToTable(TableName.Stations);
        builder.HasKey(x => x.id);

        builder.Property(x => x.officeId).IsRequired();
        builder.Property(x => x.name).IsRequired();
        builder.Property(x => x.lat).IsRequired();
        builder.Property(x => x.lng).IsRequired();

        //Each Station can have many Packages
        builder.HasMany(x => x.packages)
            .WithOne()
            .HasForeignKey(r => r.stationId)
            .OnDelete(DeleteBehavior.NoAction);

        //Each Station can have many StationRoutes
        builder.HasMany(x => x.stationRoutes)
            .WithOne()
            .HasForeignKey(r => r.stationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
