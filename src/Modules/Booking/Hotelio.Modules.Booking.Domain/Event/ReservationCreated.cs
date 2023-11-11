using Hotelio.Shared.Event;

namespace Hotelio.Modules.Booking.Domain.Event;

internal record ReservationCreated(string Id) : IEvent;