using Hotelio.Modules.Availability.Api.Services;
using Hotelio.Modules.Booking.Application.Client.Availability;
using Hotelio.Modules.Booking.Application.Event.External;
using Hotelio.Shared.Event;

namespace Hotelio.AntiCorruptionLayer.Api.Booking;

internal class AvailabilityApiClient: IAvailabilityApiClient
{
    private readonly IEventBus _eventBus;
    private readonly IAvailabilityService _availabilityService;

    public AvailabilityApiClient(IEventBus eventBus, IAvailabilityService availabilityService)
    {
        _eventBus = eventBus;
        _availabilityService = availabilityService;
    }

    public async Task Book(string hotelId, int roomType, string reservationId, DateTime startDate, DateTime endDate)
    {
        await this._availabilityService.BookFirstAvailableAsync(hotelId, roomType, reservationId, startDate, endDate);
    }

    public Task UnBook(string roomId, string reservationId, DateTime startDate, DateTime endDate)
    {
        //TODO call availabilty service
        return Task.CompletedTask;
    }
}