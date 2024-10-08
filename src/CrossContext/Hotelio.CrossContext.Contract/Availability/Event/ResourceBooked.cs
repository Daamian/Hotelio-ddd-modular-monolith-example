using Hotelio.CrossContext.Contract.Shared.Message;
using Hotelio.Shared.Event;

namespace Hotelio.CrossContext.Contract.Availability.Event;

public record ResourceBooked(string ResourceId, string OwnerId, DateTime StartDate, DateTime EndDate) : IMessage;