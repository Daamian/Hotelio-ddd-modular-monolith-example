using Hotelio.Shared.Event;

namespace Hotelio.CrossContext.Contract.Availability.Event;

public record ResourceBooked(string ResourceId, string OwnerId, string BookId) : IEvent;