using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable(TableName.Reports);
        builder.HasKey(x => x.id);

        builder.Property(x => x.content).IsRequired();
        builder.Property(x => x.createBy).IsRequired();
        builder.Property(x => x.targetId).IsRequired();
        builder.Property(x => x.createTime).HasDefaultValue(DateTime.Now);
    }
}
