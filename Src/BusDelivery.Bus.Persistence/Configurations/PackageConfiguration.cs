using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class PackageConfiguration : IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
        builder.ToTable(TableName.Package);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BusId).IsRequired();
        builder.Property(x => x.StationId).IsRequired();
        builder.Property(x => x.OfficeId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.TotalWeight).IsRequired();
        builder.Property(x => x.TotalPrice).IsRequired();
        builder.Property(x => x.Image).IsRequired();
        builder.Property(x => x.Note).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.CreateTime).HasDefaultValue(DateTime.Now);

        //Each Bus can have many Orders
        builder.HasMany(x => x.Orders)
            .WithOne()
            .HasForeignKey(r => r.PackageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
