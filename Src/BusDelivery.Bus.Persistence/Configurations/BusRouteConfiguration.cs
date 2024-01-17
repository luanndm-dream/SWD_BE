using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;

public class BusRouteConfiguration : IEntityTypeConfiguration<BusRoute>
{
    public void Configure(EntityTypeBuilder<BusRoute> builder)
    {
        builder.ToTable(TableName.BusRoute);

        builder.HasKey(x => new { x.RouteId, x.BusId });
    }
}
