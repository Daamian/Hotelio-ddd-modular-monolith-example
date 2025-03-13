using Hotelio.Modules.Pricing.Application.Exception;
using MediatR;
using Hotelio.Modules.Pricing.Domain.Model;
using Hotelio.Modules.Pricing.Domain.Repository;
using Hotelio.Shared.Domain;

namespace Hotelio.Modules.Pricing.Application.Command.Handler;

internal class AddRoomTariffHandler : IRequestHandler<AddRoomTariff>
{
    private readonly IHotelTariffRepository _repository;

    public AddRoomTariffHandler(IHotelTariffRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(AddRoomTariff request, CancellationToken cancellationToken)
    {
        var hotelTariff = await _repository.FindAsync(request.HotelTariffId)
                          ?? throw new HotelTariffNotFoundException("Hotel tariff not found.");

        var roomTariff = RoomTariff.Create(request.RoomTypeId, Price.CreateDefault(request.BasePriceNetAmount));
        hotelTariff.AddRoomTariff(roomTariff);

        await _repository.UpdateAsync(hotelTariff);
    }
}