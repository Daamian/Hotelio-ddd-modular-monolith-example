namespace Hotelio.Shared.Event;

public interface IEventBus
{
    public Task Publish(IEvent eventItem);
}