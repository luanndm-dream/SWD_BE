using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class CoordinateConfiguration : IEntityTypeConfiguration<Coordinate>
{
    public void Configure(EntityTypeBuilder<Coordinate> builder)
    {
        builder.ToTable(TableName.Coordinates);
        builder.HasKey(x => x.id);

        builder.Property(x => x.lat).IsRequired();
        builder.Property(x => x.lng).IsRequired();
        builder.Property(x => x.stt).IsRequired();
        builder.Property(x => x.routeId).IsRequired();
    }
}
