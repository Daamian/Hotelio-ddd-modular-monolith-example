using Hotelio.Modules.Pricing.Application.Exception;
using MediatR;
using Hotelio.Modules.Pricing.Domain.Model;
using Hotelio.Modules.Pricing.Domain.Repository;
using Hotelio.Shared.Domain;

namespace Hotelio.Modules.Pricing.Application.Command.Handler;

internal class AddRoomPeriodPriceHandler : IRequestHandler<AddRoomPeriodPrice>
{
    private readonly IHotelTariffRepository _repository;

    public AddRoomPeriodPriceHandler(IHotelTariffRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(AddRoomPeriodPrice request, CancellationToken cancellationToken)
    {
        var hotelTariff = await _repository.FindAsync(request.HotelTariffId.ToString())
                          ?? throw new HotelTariffNotFoundException("Hotel tariff not found.");
        
        var roomTariff = hotelTariff.RoomTariffs.FirstOrDefault(rt => rt.RoomTypeId == request.RoomTypeId)
                         ?? throw new RoomTariffNotFoundException("Room tariff not found.");
        
        roomTariff.AddPeriodPrice(
            Price.CreateDefault(request.PriceNetAmount),
            new Period(request.StartDate, request.EndDate));
        
        await _repository.SaveAsync(hotelTariff);
    }
}