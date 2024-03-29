namespace Hotelio.Shared.Event;

using MediatR;

public class MediatREventBus: IEventBus
{
    private readonly IMediator _mediator;

    public MediatREventBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Publish(IEvent eventItem)
    {
        await _mediator.Publish(eventItem);
    }
}