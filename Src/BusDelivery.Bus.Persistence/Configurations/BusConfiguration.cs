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
        builder.Property(x => x.NumberOfSeat).IsRequired();
        builder.Property(x => x.OperateTime).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();

        //Each Bus can have many Routes
        builder.HasMany(x => x.Packages)
            .WithOne()
            .HasForeignKey(p => p.BusId)
            .OnDelete(DeleteBehavior.NoAction);

        //Each Bus can have many BusRoutes
        builder.HasMany(x => x.BusRoutes)
            .WithOne()
            .HasForeignKey(r => r.BusId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
