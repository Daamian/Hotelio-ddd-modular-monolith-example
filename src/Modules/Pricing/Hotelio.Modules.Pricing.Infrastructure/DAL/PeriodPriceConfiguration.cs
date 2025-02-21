using Hotelio.Modules.Pricing.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelio.Modules.Pricing.Infrastructure.DAL;

internal class PeriodPriceConfiguration : IEntityTypeConfiguration<PeriodPrice>
{
    public void Configure(EntityTypeBuilder<PeriodPrice> builder)
    {
        builder.HasKey(pp => pp.Id);
        builder.Property(pp => pp.Id).ValueGeneratedNever();

        builder.OwnsOne(pp => pp.Period, period =>
        {
            period.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
            period.Property(p => p.EndDate).HasColumnName("EndDate").IsRequired();
        });

        builder.OwnsOne(pp => pp.Price, price =>
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