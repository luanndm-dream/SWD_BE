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
        builder.Property(x => x.Lat).IsRequired();
        builder.Property(x => x.Lng).IsRequired();
        builder.Property(x => x.Contact).IsRequired();
        builder.Property(x => x.Image).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();

        //Each Office can have many Stations
        builder.HasMany(x => x.Stations)
            .WithOne()
            .HasForeignKey(r => r.OfficeId)
            .OnDelete(DeleteBehavior.NoAction);

        //Each Office can have many Users
        builder.HasMany(x => x.Users)
            .WithOne()
            .HasForeignKey(r => r.OfficeId)
            .OnDelete(DeleteBehavior.NoAction);

        //Each Office can have many Weathers
        builder.HasMany(x => x.Weathers)
            .WithOne()
            .HasForeignKey(r => r.OfficeId)
            .OnDelete(DeleteBehavior.NoAction);

        //Each Office can have many PackagesFrom
        builder.HasMany(x => x.PackagesFrom)
            .WithOne()
            .HasForeignKey(p => p.FromOfficeId)
            .OnDelete(DeleteBehavior.NoAction);

        //Each Office can have many PackagesTo
        builder.HasMany(x => x.PackagesTo)
            .WithOne()
            .HasForeignKey(p => p.ToOfficeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
