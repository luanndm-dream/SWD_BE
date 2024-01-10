using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class UserPackageConfiguration : IEntityTypeConfiguration<UserPackage>
{
    public void Configure(EntityTypeBuilder<UserPackage> builder)
    {
        builder.ToTable(TableName.UserPackages);

        builder.HasKey(x => new { x.userId, x.packageId });
    }
}
