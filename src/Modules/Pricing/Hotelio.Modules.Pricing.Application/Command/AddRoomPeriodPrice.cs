using MediatR;

namespace Hotelio.Modules.Pricing.Application.Command;

internal record AddRoomPeriodPrice(
    Guid HotelTariffId,
    string RoomTypeId,
    decimal PriceNetAmount,
    DateTime StartDate,
    DateTime EndDate) : IRequest;