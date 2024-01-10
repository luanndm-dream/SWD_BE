using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableName.Roles);
        builder.HasKey(x => x.id);

        builder.Property(x => x.name).IsRequired();
        builder.Property(x => x.description).IsRequired();

        //Each Role can have many Users
        builder.HasMany(x => x.users)
            .WithOne()
            .HasForeignKey(r => r.roleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
