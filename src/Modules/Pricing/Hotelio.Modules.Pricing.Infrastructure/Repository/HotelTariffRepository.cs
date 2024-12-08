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

    public async Task<HotelTariff?> FindAsync(string hotelId)
    {
        return await _context.HotelTariffs
            .Include(ht => ht.RoomTariffs)
            .ThenInclude(rt => rt.PeriodPrices)
            .Include(ht => ht.AmenityTariffs)
            .FirstOrDefaultAsync(ht => ht.HotelId == hotelId);
    }

    public async Task SaveAsync(HotelTariff hotelTariff)
    {
        var existing = await _context.HotelTariffs.FindAsync(hotelTariff.Id);
        if (existing == null)
        {
            await _context.HotelTariffs.AddAsync(hotelTariff);
        }
        else
        {
            _context.HotelTariffs.Update(hotelTariff);
        }

        await _context.SaveChangesAsync();
    }
}