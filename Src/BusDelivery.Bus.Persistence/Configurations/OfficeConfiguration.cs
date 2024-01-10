using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.ToTable(TableName.Offices);
        builder.HasKey(x => x.id);

        builder.Property(x => x.routeId).IsRequired();
        builder.Property(x => x.name).IsRequired();
        builder.Property(x => x.address).IsRequired();
        builder.Property(x => x.lat).HasMaxLength(10);
        builder.Property(x => x.lng).HasMaxLength(15);
        builder.Property(x => x.contact).IsRequired();
        builder.Property(x => x.images).IsRequired();
        builder.Property(x => x.status).HasDefaultValue(true);

        //Each Office can have many Weathers
        builder.HasMany(x => x.weathers)
            .WithOne()
            .HasForeignKey(r => r.officeId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Office can have many Routes
        builder.HasMany(x => x.routes)
            .WithOne()
            .HasForeignKey(r => r.officeId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Office can have many Users
        builder.HasMany(x => x.users)
            .WithOne()
            .HasForeignKey(r => r.officeId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Office can have many Users
        builder.HasMany(x => x.officePackages)
            .WithOne()
            .HasForeignKey(r => r.officeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
