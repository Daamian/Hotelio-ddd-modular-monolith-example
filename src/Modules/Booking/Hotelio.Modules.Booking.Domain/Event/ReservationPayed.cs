using Hotelio.Shared.Event;

namespace Hotelio.Modules.Booking.Domain.Event;

public record ReservationPayed(string Id): IEvent;