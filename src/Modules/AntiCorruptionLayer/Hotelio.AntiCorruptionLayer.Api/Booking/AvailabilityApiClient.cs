using Hotelio.Modules.Booking.Application.Client.Availability;
using Hotelio.Modules.Booking.Application.Event.External;
using Hotelio.Shared.Event;

namespace Hotelio.AntiCorruptionLayer.Api.Booking;

internal class AvailabilityApiClient: IAvailabilityApiClient
{
    private readonly IEventBus _eventBus;

    public AvailabilityApiClient(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task Book(string hotelId, string roomType, string reservationId, DateTime startDate, DateTime endDate)
    {
        this._eventBus.publish(new RoomBooked("roomId", reservationId));
        return Task.CompletedTask;
    }

    public Task UnBook(string roomId, string reservationId, DateTime startDate, DateTime endDate)
    {
        return Task.CompletedTask;
    }
}