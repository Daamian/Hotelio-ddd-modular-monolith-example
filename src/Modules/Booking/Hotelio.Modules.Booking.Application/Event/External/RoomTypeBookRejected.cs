using Hotelio.Shared.Event;

namespace Hotelio.Modules.Booking.Application.Event.External;

public record RoomTypeBookRejected(string ReservationId, string HotelId, string RoomType) : IEvent;