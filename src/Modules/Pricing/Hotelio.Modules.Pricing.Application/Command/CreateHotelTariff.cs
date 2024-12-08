using MediatR;

namespace Hotelio.Modules.Pricing.Application.Command;

internal record CreateHotelTariff(string HotelId, double BasePriceNetAmount) : IRequest<Guid>;