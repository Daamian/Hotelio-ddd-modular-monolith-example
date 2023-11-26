using Hotelio.Shared.Domain;
using MediatR;

namespace Hotelio.Shared.Event;

public interface IEventBus
{
    public Task publish(IEvent eventItem);
}