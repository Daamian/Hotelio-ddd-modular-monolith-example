using Hotelio.Shared.Domain;
using MediatR;

namespace Hotelio.Shared.Event;

public interface IEventBus
{
    public void publish(IEvent eventItem);
}