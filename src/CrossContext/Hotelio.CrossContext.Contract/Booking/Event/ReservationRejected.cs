using Hotelio.CrossContext.Contract.Shared.Message;

namespace Hotelio.CrossContext.Contract.Booking.Event;

public record ReservationRejected(string Id): IMessage;