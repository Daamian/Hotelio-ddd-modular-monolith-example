using Hotelio.Shared.Event;

namespace Hotelio.CrossContext.Contract.Availability.Event;

public record ResourceTypeBookRejected(string OwnerId, string GroupId, string Type) : IEvent;