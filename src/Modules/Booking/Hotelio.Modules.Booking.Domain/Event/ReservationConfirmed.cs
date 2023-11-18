using Hotelio.Shared.Event;

namespace Hotelio.Modules.Booking.Domain.Event;

internal record ReservationConfirmed(string ReservationId): IEvent;