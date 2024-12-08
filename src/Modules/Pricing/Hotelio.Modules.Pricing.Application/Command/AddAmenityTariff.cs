using MediatR;

namespace Hotelio.Modules.Pricing.Application.Command;

internal record AddAmenityTariff(Guid HotelTariffId, string AmenityId, double PriceNetAmount) : IRequest;