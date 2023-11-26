namespace Hotelio.Shared.Event;

using MediatR;

public class MediatREventBus: IEventBus
{
    private IMediator _mediator;

    public MediatREventBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task publish(IEvent eventItem)
    {
        await this._mediator.Publish(eventItem);
    }
}