using Hotelio.Shared.Event;

namespace Hotelio.Modules.Booking.Domain.Event;

internal record ReservationCanceled(string ReservationId) : IEvent;