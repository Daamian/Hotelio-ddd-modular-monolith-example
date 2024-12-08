using Hotelio.Modules.Pricing.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelio.Modules.Pricing.Infrastructure.DAL;

internal class RoomTariffConfiguration : IEntityTypeConfiguration<RoomTariff>
{
    public void Configure(EntityTypeBuilder<RoomTariff> builder)
    {
        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.RoomTypeId).IsRequired();
        builder.OwnsOne(rt => rt.BasePrice, bp =>
        {
            bp.Property(p => p.NetAmount)
                .HasColumnName("BasePriceNetAmount")
                .IsRequired();

            bp.Property(p => p.Currency)
                .HasColumnName("BasePriceCurrency")
                .HasConversion<string>()
                .IsRequired();

            bp.Property(p => p.TaxRate)
                .HasColumnName("BasePriceTaxRate")
                .IsRequired();
        });

        builder.HasMany(rt => rt.PeriodPrices)
            .WithOne()
            .HasForeignKey("RoomTariffId");
    }
}