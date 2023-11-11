namespace Hotelio.Shared.Event;

using MediatR;

public class MediatREventBus: IEventBus
{
    private Mediator _mediator;

    public MediatREventBus(Mediator mediator)
    {
        _mediator = mediator;
    }

    public void publish(IEvent eventItem)
    {
        this._mediator.Publish(eventItem);
    }
}