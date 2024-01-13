using BusDelivery.Contract.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class BusConfiguration : IEntityTypeConfiguration<Domain.Entities.Bus>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Bus> builder)
    {
        builder.ToTable(TableName.Bus);
        builder.HasKey(x => x.id);

        builder.Property(x => x.number).IsRequired();
        builder.Property(x => x.plateNumber).IsRequired();
        builder.Property(x => x.name).IsRequired();
        builder.Property(x => x.organization).IsRequired();
        builder.Property(x => x.color).IsRequired();
        builder.Property(x => x.numberOfSeat).HasMaxLength(10);
        builder.Property(x => x.operateTime).HasMaxLength(15);
        builder.Property(x => x.status).HasDefaultValue(true);

        //Each Bus can have many Routes
        builder.HasMany(x => x.packages)
            .WithOne()
            .HasForeignKey(p => p.busId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Bus can have many BusRoutes
        builder.HasMany(x => x.busRoutes)
            .WithOne()
            .HasForeignKey(r => r.busId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
