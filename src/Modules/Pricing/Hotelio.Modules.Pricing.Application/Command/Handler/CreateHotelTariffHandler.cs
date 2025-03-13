using MediatR;
using Hotelio.Modules.Pricing.Domain.Model;
using Hotelio.Modules.Pricing.Domain.Repository;
using Hotelio.Shared.Domain;

namespace Hotelio.Modules.Pricing.Application.Command.Handler;

internal class CreateHotelTariffHandler : IRequestHandler<CreateHotelTariff, Guid>
{
    private readonly IHotelTariffRepository _repository;

    public CreateHotelTariffHandler(IHotelTariffRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateHotelTariff request, CancellationToken cancellationToken)
    {
        var hotelTariff = HotelTariff.Create(request.HotelId, Price.CreateDefault(request.BasePriceNetAmount));
        await _repository.SaveAsync(hotelTariff);
        return hotelTariff.Id;
    }
}