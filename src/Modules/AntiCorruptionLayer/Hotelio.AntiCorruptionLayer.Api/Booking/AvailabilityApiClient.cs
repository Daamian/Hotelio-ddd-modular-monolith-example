using Hotelio.Modules.Availability.Api.Services;
using Hotelio.Modules.Booking.Application.Client.Availability;
using Hotelio.Shared.Event;

namespace Hotelio.AntiCorruptionLayer.Api.Booking;

internal class AvailabilityApiClient: IAvailabilityApiClient
{
    private readonly IAvailabilityService _availabilityService;

    public AvailabilityApiClient(IAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    public async Task Book(string hotelId, int roomType, string reservationId, DateTime startDate, DateTime endDate)
    {
        await this._availabilityService.BookFirstAvailableAsync(hotelId, roomType, reservationId, startDate, endDate);
    }

    public async Task UnBook(string roomId, string reservationId, DateTime startDate, DateTime endDate)
    {
        await this._availabilityService.UnBookAsync(roomId, reservationId);
    }
}