using BusDelivery.Contract.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class PathConfiguration : IEntityTypeConfiguration<Domain.Entities.Path>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Path> builder)
    {
        builder.ToTable(TableName.Paths);
        builder.HasKey(x => x.id);

        builder.Property(x => x.lt).IsRequired();
        builder.Property(x => x.ln).IsRequired();
        builder.Property(x => x.stt).IsRequired();
        builder.Property(x => x.routeId).IsRequired();
    }
}
