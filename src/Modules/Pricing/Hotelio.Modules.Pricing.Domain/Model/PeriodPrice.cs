using Hotelio.Shared.Domain;

namespace Hotelio.Modules.Pricing.Domain.Model;

internal class PeriodPrice
{
    public Guid Id { get; private set; }
    public Period Period { get; private set; }
    public Price Price { get; private set; }
    
    protected PeriodPrice() {}

    private PeriodPrice(Guid id, Period period, Price price)
    {
        Id = id;
        Period = period;
        Price = price;
    }

    public static PeriodPrice Create(Period period, Price price)
    {
        return new PeriodPrice(Guid.NewGuid(), period, price);
    }
}