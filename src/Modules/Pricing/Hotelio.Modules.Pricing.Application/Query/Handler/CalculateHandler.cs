using Hotelio.Modules.Pricing.Application.Exception;
using Hotelio.Modules.Pricing.Domain.Repository;
using MediatR;

namespace Hotelio.Modules.Pricing.Application.Query.Handler;

internal class CalculateHandler : IRequestHandler<Calculate, double>
{
    private readonly IHotelTariffRepository _hotelTariffRepository;
    
    public CalculateHandler(IHotelTariffRepository hotelTariffRepository) => this._hotelTariffRepository = hotelTariffRepository;
    
    public async Task<double> Handle(Calculate request, CancellationToken cancellationToken)
    {
        //TODO implement query to get by HotelId
        var hotelTariff = await _hotelTariffRepository.FindAsync(new Guid(request.HotelId));

        if (hotelTariff is null)
        {
            throw new TariffForHotelNotFoundException("Not found tariff for this hotel.");
        }
        
        return hotelTariff.Calculate(request.RoomType, request.Amenities, request.StartDate, request.EndDate);
    }
}