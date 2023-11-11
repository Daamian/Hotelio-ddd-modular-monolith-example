using Hotelio.Shared.Event;

namespace Hotelio.Shared.Domain;

public abstract class Aggregate
{
    public List<IEvent> Events { get; protected set; } = new List<IEvent>();
}