using BusDelivery.Contract.Constants;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusDelivery.Persistence.Configurations;
internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(TableName.RefreshTokens);

        builder.HasKey(rt => rt.id);

        builder.Property(rt => rt.userId).IsRequired();
        builder.Property(rt => rt.clientId).IsRequired();
        builder.Property(rt => rt.deviceId).IsRequired();
        builder.Property(rt => rt.token).IsRequired();
        builder.Property(rt => rt.expiresOn).IsRequired();
        builder.Property(rt => rt.createdOn).IsRequired();

        builder.HasIndex(rt => rt.token).IsUnique();

        builder.HasIndex(rt => new { rt.clientId, rt.deviceId }).IsUnique();
    }
}
