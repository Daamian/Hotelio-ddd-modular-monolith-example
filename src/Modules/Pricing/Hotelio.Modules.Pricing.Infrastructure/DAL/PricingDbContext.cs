using Hotelio.Modules.Pricing.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.Pricing.Infrastructure.DAL;

internal class PricingDbContext : DbContext
{
    public DbSet<HotelTariff> HotelTariffs { get; set; }

    public PricingDbContext(DbContextOptions<PricingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("pricing");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.ApplyConfiguration(new HotelTariffConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTariffConfiguration());
        modelBuilder.ApplyConfiguration(new AmenityTariffConfiguration());
        modelBuilder.ApplyConfiguration(new PeriodPriceConfiguration());
    }
}