using Hotelio.Shared.Event;

namespace Hotelio.Modules.Availability.Domain.Event;

internal record ResourceBooked(string Id, string OwnerId, DateTime StartDate, DateTime EndDate) : IEvent;