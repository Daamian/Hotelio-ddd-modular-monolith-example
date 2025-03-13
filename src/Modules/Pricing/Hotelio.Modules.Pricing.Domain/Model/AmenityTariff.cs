using Hotelio.Shared.Domain;

namespace Hotelio.Modules.Pricing.Domain.Model;

internal class AmenityTariff
{
    public Guid Id { get; private set; }
    public string AmenityId { get; private set; }
    public Price Price { get; private set; }
    
    protected AmenityTariff() {}

    private AmenityTariff(Guid id, string amenityId, Price price)
    {
        Id = id;
        AmenityId = amenityId;
        Price = price;
    }

    public static AmenityTariff Create(string amenityId, Price price)
    {
        return new AmenityTariff(Guid.NewGuid(), amenityId, price);
    }
}