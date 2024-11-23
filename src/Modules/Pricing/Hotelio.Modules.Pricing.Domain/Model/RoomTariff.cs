using Hotelio.Shared.Domain;
using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Pricing.Domain.Model;

internal class RoomTariff
{
    public Guid Id { get; private set; }
    public string RoomTypeId { get; private set; }
    public Price BasePrice { get; private set; }
    private readonly List<PeriodPrice> _periodPrices = new List<PeriodPrice>();
    public IReadOnlyList<PeriodPrice> PeriodPrices => _periodPrices.AsReadOnly();

    private RoomTariff(Guid id, string roomTypeId, Price basePrice)
    {
        Id = id;
        RoomTypeId = roomTypeId;
        BasePrice = basePrice;
    }

    public static RoomTariff Create(string roomTypeId, Price basePrice)
    {
        return new RoomTariff(Guid.NewGuid(), roomTypeId, basePrice);
    }

    public void AddPeriodPrice(Price price, Period period)
    {
        if (_periodPrices.Any(pp => pp.Period.Overlaps(period)))
            throw new DomainException("Periods cannot overlap.");

        _periodPrices.Add(PeriodPrice.Create(period, price));
    }

    public Price? GetPriceForPeriod(DateTime startDate, DateTime endDate)
    {
        var periodPrice = _periodPrices.FirstOrDefault(pp =>
            pp.Period.StartDate <= startDate && pp.Period.EndDate >= endDate);
        return periodPrice?.Price;
    }
}