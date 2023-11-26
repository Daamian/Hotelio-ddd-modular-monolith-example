using Hotelio.Modules.Availability.Domain.Event;
using Hotelio.Modules.Booking.Application.Event.External;
using Hotelio.Shared.Event;
using MediatR;

namespace Hotelio.AntiCorruptionLayer.EventMapping.Booking;

internal class AvailabilityEventMapping : INotificationHandler<ResourceBooked>
{
    private readonly IEventBus _eventBus;

    public AvailabilityEventMapping(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(ResourceBooked domainEvent, CancellationToken cancellationToken)
    {
        await this._eventBus.publish(new RoomBooked(domainEvent.Id, domainEvent.OwnerId));
    }
}