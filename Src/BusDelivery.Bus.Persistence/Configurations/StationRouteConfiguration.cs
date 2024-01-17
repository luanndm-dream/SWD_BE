using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class StationRouteConfiguration : IEntityTypeConfiguration<StationRoute>
{
    public void Configure(EntityTypeBuilder<StationRoute> builder)
    {
        builder.ToTable(TableName.StationRoute);
        builder.HasKey(x => new { x.RouteId, x.StationId });
    }
}
