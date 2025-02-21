using Hotelio.Modules.Pricing.Domain.Model;

namespace Hotelio.Modules.Pricing.Domain.Repository;

internal interface IHotelTariffRepository
{
    Task<HotelTariff?> FindAsync(Guid id);
    Task SaveAsync(HotelTariff hotelTariff);
    Task UpdateAsync(HotelTariff hotelTariff);
}