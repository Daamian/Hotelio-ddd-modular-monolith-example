using Hotelio.CrossContext.Contract.Pricing;
using Hotelio.Modules.Pricing.Application.Query;
using Hotelio.Shared.Queries;

namespace Hotelio.Modules.Pricing.Api.CrossContext;

internal class PricingService: IPricingService
{
    private readonly IQueryBus _queryBus;
    
    public PricingService(IQueryBus queryBus) => _queryBus = queryBus;

    public async Task<double> CalculatePrice(string hotelId, string roomType, List<string> amenities, int guests, DateTime startDate, DateTime endDate)
    {
        return await _queryBus.QueryAsync(new Calculate(hotelId, roomType, amenities, startDate, endDate));
    }
}