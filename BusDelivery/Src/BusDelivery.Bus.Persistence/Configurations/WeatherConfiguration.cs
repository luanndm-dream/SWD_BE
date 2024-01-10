using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
internal class WeatherConfiguration : IEntityTypeConfiguration<Weather>
{
    public void Configure(EntityTypeBuilder<Weather> builder)
    {
        builder.ToTable(TableName.Weathers);
        builder.HasKey(x => x.id);

        builder.Property(x => x.officeId).IsRequired();
        builder.Property(x => x.temperature).IsRequired();
        builder.Property(x => x.humidity).IsRequired();
        builder.Property(x => x.windySpeed).IsRequired();
        builder.Property(x => x.recordAt).IsRequired();
    }
}

