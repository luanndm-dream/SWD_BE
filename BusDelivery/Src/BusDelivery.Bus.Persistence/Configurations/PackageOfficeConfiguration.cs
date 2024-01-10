using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class PackageOfficeConfiguration : IEntityTypeConfiguration<OfficePackage>
{
    public void Configure(EntityTypeBuilder<OfficePackage> builder)
    {
        builder.ToTable(TableName.OfficePackages);

        builder.HasKey(x => new { x.packageId, x.officeId });
    }
}
