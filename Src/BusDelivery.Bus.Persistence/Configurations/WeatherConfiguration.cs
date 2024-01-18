using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
internal class WeatherConfiguration : IEntityTypeConfiguration<Weather>
{
    public void Configure(EntityTypeBuilder<Weather> builder)
    {
        builder.ToTable(TableName.Weather);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OfficeId).IsRequired();
        builder.Property(x => x.Temperature).IsRequired();
        builder.Property(x => x.Humidity).IsRequired();
        builder.Property(x => x.WindySpeed).IsRequired();
        builder.Property(x => x.RecordAt).IsRequired();
    }
}
