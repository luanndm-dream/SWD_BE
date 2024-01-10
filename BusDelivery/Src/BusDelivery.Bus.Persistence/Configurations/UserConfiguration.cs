using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableName.Users);
        builder.HasKey(x => x.id);

        builder.Property(x => x.roleId).IsRequired();
        builder.Property(x => x.officeId).IsRequired();
        builder.Property(x => x.name).IsRequired();
        builder.Property(x => x.email).IsRequired();
        builder.Property(x => x.phoneNumber).IsRequired();
        builder.Property(x => x.gentle).IsRequired();
        builder.Property(x => x.status).HasDefaultValue(true);
        builder.Property(x => x.deviceId).IsRequired();
        builder.Property(x => x.deviceVersion).IsRequired();
        builder.Property(x => x.OS).IsRequired();

        //Each User can have many RefreshToken
        builder.HasMany(x => x.refreshTokens)
            .WithOne()
            .HasForeignKey(r => r.userId)
            .OnDelete(DeleteBehavior.Cascade);

        //Each User can have many RefreshToken
        builder.HasMany(x => x.reports)
            .WithOne()
            .HasForeignKey(r => r.createBy)
            .OnDelete(DeleteBehavior.Cascade);

        //Each User can have many RefreshToken
        builder.HasMany(x => x.userPackages)
            .WithOne()
            .HasForeignKey(r => r.userId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
