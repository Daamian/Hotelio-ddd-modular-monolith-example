using Hotelio.Modules.Pricing.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelio.Modules.Pricing.Infrastructure.DAL;

internal class AmenityTariffConfiguration : IEntityTypeConfiguration<AmenityTariff>
{
    public void Configure(EntityTypeBuilder<AmenityTariff> builder)
    {
        builder.HasKey(at => at.Id);
        builder.Property(at => at.AmenityId).IsRequired();
        builder.OwnsOne(at => at.Price, price =>
        {
            price.Property(p => p.NetAmount)
                .HasColumnName("PriceNetAmount")
                .IsRequired();

            price.Property(p => p.Currency)
                .HasColumnName("PriceCurrency")
                .HasConversion<string>() // Konwersja na string w bazie danych
                .IsRequired();

            price.Property(p => p.TaxRate)
                .HasColumnName("PriceTaxRate")
                .IsRequired();
        });
    }
}