using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableName.User);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RoleId).IsRequired();
        builder.Property(x => x.OfficeId).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(X => X.HashPassword).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();
        builder.Property(x => x.Gentle).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreateTime).IsRequired();

        //Each User can have many Reports
        builder.HasMany(x => x.Reports)
            .WithOne()
            .HasForeignKey(r => r.CreateBy)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
