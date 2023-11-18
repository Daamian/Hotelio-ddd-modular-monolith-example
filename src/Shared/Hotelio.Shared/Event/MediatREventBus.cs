namespace Hotelio.Shared.Event;

using MediatR;

public class MediatREventBus: IEventBus
{
    private IMediator _mediator;

    public MediatREventBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void publish(IEvent eventItem)
    {
        this._mediator.Publish(eventItem);
    }
}