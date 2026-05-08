namespace Hotelio.CrossContext.Contract.Pricing;

public interface IPricingService
{
    public Task<decimal> CalculatePrice(string hotelId, string roomType, List<string> amenities, int guests, DateTime startDate, DateTime endDate);
}