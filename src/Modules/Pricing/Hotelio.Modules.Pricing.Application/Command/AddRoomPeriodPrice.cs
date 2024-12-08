using MediatR;

namespace Hotelio.Modules.Pricing.Application.Command;

internal record AddRoomPeriodPrice(
    Guid HotelTariffId,
    string RoomTypeId,
    double PriceNetAmount,
    DateTime StartDate,
    DateTime EndDate) : IRequest;