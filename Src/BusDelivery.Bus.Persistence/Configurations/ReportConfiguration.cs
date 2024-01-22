using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable(TableName.Report);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.CreateBy).IsRequired();
        builder.Property(x => x.TargetId).IsRequired();
        builder.Property(x => x.CreateTime).IsRequired();
        builder.Property(x => x.Type).IsRequired();
    }
}
