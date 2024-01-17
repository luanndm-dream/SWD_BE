using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TableName.Order);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PackageId).IsRequired();
        builder.Property(x => x.Image).IsRequired();
        builder.Property(x => x.Weight).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Note).IsRequired();
        builder.Property(x => x.Contact).IsRequired();
    }
}
