using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class PackageConfiguration : IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
        builder.ToTable(TableName.Packages);
        builder.HasKey(x => x.id);

        builder.Property(x => x.userId).IsRequired();
        builder.Property(x => x.quantity).IsRequired();
        builder.Property(x => x.officeId).IsRequired();
        builder.Property(x => x.weight).IsRequired();
        builder.Property(x => x.price).IsRequired();
        builder.Property(x => x.image).IsRequired();
        builder.Property(x => x.note).IsRequired();
        builder.Property(x => x.status).HasDefaultValue(true);
        builder.Property(x => x.createTime).HasDefaultValue(DateTime.Now);

        //Each Bus can have many Routes
        builder.HasMany(x => x.userPackages)
            .WithOne()
            .HasForeignKey(r => r.packageId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Bus can have many Routes
        builder.HasMany(x => x.officePackages)
            .WithOne()
            .HasForeignKey(r => r.packageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
