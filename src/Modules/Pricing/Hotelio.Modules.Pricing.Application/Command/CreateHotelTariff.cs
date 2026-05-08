using MediatR;

namespace Hotelio.Modules.Pricing.Application.Command;

internal record CreateHotelTariff(string HotelId, decimal BasePriceNetAmount) : IRequest<Guid>;