using Hotelio.Shared.Event;

namespace Hotelio.Modules.Booking.Application.Event.External;

public record RoomBooked(string RoomId, string ReservationId) : IEvent;