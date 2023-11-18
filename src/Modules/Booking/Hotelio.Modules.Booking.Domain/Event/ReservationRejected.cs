using Hotelio.Shared.Event;

namespace Hotelio.Modules.Booking.Domain.Event;

internal record ReservationRejected(string ReservationId): IEvent;