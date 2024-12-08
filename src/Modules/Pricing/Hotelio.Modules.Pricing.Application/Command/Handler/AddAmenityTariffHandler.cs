using Hotelio.Modules.Pricing.Application.Exception;
using MediatR;
using Hotelio.Modules.Pricing.Domain.Model;
using Hotelio.Modules.Pricing.Domain.Repository;
using Hotelio.Shared.Domain;

namespace Hotelio.Modules.Pricing.Application.Command.Handler;

internal class AddAmenityTariffHandler : IRequestHandler<AddAmenityTariff>
{
    private readonly IHotelTariffRepository _repository;

    public AddAmenityTariffHandler(IHotelTariffRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(AddAmenityTariff request, CancellationToken cancellationToken)
    {
        var hotelTariff = await _repository.FindAsync(request.HotelTariffId.ToString())
                          ?? throw new HotelTariffNotFoundException("Hotel tariff not found.");

        var amenityTariff = AmenityTariff.Create(request.AmenityId, Price.CreateDefault(request.PriceNetAmount));
        hotelTariff.AddAmenityTariff(amenityTariff);

        await _repository.SaveAsync(hotelTariff);
    }
}