using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class StationConfiguration : IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.ToTable(TableName.Station);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OfficeId).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Lat).IsRequired();
        builder.Property(x => x.Lng).IsRequired();
        builder.Property(x => x.IsActive).HasDefaultValue(true);

        //Each Station can have many Packages
        builder.HasMany(x => x.Packages)
            .WithOne()
            .HasForeignKey(r => r.StationId)
            .OnDelete(DeleteBehavior.NoAction);

        //Each Station can have many StationRoutes
        builder.HasMany(x => x.StationRoutes)
            .WithOne()
            .HasForeignKey(r => r.StationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
