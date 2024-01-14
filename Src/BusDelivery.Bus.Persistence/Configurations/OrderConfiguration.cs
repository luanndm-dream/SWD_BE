using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TableName.Orders);
        builder.HasKey(x => x.id);

        builder.Property(x => x.packageId).IsRequired();
        builder.Property(x => x.image).IsRequired();
        builder.Property(x => x.weight).IsRequired();
        builder.Property(x => x.price).IsRequired();
        builder.Property(x => x.note).IsRequired();
        builder.Property(x => x.contact).IsRequired();
    }
}
