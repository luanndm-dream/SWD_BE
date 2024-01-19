using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.ToTable(TableName.Office);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.Lat).HasMaxLength(10);
        builder.Property(x => x.Lng).HasMaxLength(15);
        builder.Property(x => x.Contact).IsRequired();
        builder.Property(x => x.Image).IsRequired();
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        //Each Office can have many Stations
        builder.HasMany(x => x.Stations)
            .WithOne()
            .HasForeignKey(r => r.OfficeId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Office can have many Users
        builder.HasMany(x => x.Users)
            .WithOne()
            .HasForeignKey(r => r.OfficeId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each Office can have many Weathers
        builder.HasMany(x => x.Weathers)
            .WithOne()
            .HasForeignKey(r => r.OfficeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
