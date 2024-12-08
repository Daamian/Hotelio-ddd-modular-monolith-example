using Hotelio.Modules.Pricing.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelio.Modules.Pricing.Infrastructure.DAL;

internal class HotelTariffConfiguration : IEntityTypeConfiguration<HotelTariff>
{
    public void Configure(EntityTypeBuilder<HotelTariff> builder)
    {
        builder.HasKey(ht => ht.Id);
        builder.Property(ht => ht.HotelId).IsRequired();
        builder.Ignore(ht => ht.Events);
        
        builder.OwnsOne(ht => ht.BasePrice, bp =>
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

        builder.HasMany(ht => ht.RoomTariffs)
            .WithOne()
            .HasForeignKey("HotelTariffId");

        builder.HasMany(ht => ht.AmenityTariffs)
            .WithOne()
            .HasForeignKey("HotelTariffId");
    }
}