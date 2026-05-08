using MediatR;

namespace Hotelio.Modules.Pricing.Application.Command;

internal record AddRoomTariff(Guid HotelTariffId, string RoomTypeId, decimal BasePriceNetAmount) : IRequest;