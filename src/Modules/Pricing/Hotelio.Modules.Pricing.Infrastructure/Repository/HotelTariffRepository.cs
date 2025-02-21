using Hotelio.Modules.Pricing.Domain.Model;
using Hotelio.Modules.Pricing.Domain.Repository;
using Hotelio.Modules.Pricing.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.Pricing.Infrastructure.Repository;

internal class HotelTariffRepository : IHotelTariffRepository
{
    private readonly PricingDbContext _context;

    public HotelTariffRepository(PricingDbContext context)
    {
        _context = context;
    }

    public async Task<HotelTariff?> FindAsync(Guid id)
    {
        return await _context.HotelTariffs
            .Include(ht => ht.RoomTariffs)
            .ThenInclude(rt => rt.PeriodPrices)
            .Include(ht => ht.AmenityTariffs)
            .FirstOrDefaultAsync(ht => ht.Id == id);
    }

    public async Task SaveAsync(HotelTariff hotelTariff)
    {
        await _context.HotelTariffs.AddAsync(hotelTariff);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(HotelTariff hotelTariff)
    {
        _context.HotelTariffs.Attach(hotelTariff);
        await _context.SaveChangesAsync();
    }
}