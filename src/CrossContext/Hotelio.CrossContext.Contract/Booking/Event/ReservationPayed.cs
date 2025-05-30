using Hotelio.CrossContext.Contract.Shared.Message;

namespace Hotelio.CrossContext.Contract.Booking.Event;

public record ReservationPayed(string Id, bool IsPayInAdvance): IMessage;