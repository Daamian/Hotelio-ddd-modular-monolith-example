using Hotelio.Modules.Availability.Domain.Event;
using Hotelio.Shared.Event;
using ResourceBookedContract = Hotelio.CrossContext.Contract.Availability.Event.ResourceBooked;
using MediatR;

namespace Hotelio.Modules.Availability.Api.CrossContext;

internal class EventPublisher: INotificationHandler<ResourceBooked>
{
    private readonly IEventBus _eventBus;

    public EventPublisher(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public async Task Handle(ResourceBooked domainEvent, CancellationToken cancelationToken)
    {
        await _eventBus.Publish(new ResourceBookedContract(domainEvent.Id, domainEvent.OwnerId));
    }
}