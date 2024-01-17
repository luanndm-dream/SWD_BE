using BusDelivery.Contract.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class BusConfiguration : IEntityTypeConfiguration<Domain.Entities.Bus>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Bus> builder)
    {
        builder.ToTable(TableName.Bus);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Number).IsRequired();
        builder.Property(x => x.PlateNumber).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Organization).IsRequired();
        builder.Property(x => x.Color).IsRequired();
        builder.Property(x => x.NumberOfSeat).HasMaxLength(10);
        builder.Property(x => x.OperateTime).HasMaxLength(15);
        builder.Property(x => x.Status).HasDefaultValue(true);

        //Each Bus can have many Routes
        builder.HasMany(x => x.Packages)
            .WithOne()
            .HasForeignKey(p => p.BusId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Bus can have many BusRoutes
        builder.HasMany(x => x.BusRoutes)
            .WithOne()
            .HasForeignKey(r => r.BusId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
