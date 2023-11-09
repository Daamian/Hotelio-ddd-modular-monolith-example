using Hotelio.Modules.Booking.Application.Client.Availability;

namespace Hotelio.AntiCorruptionLayer.Api.Booking;

internal class AvailabilityApiClient: IAvailabilityApiClient
{
    public Task Book(string roomId, string reservationId, DateTime startDate, DateTime endDate)
    {
        return Task.CompletedTask;
    }

    public Task UnBook(string roomId, string reservationId, DateTime startDate, DateTime endDate)
    {
        return Task.CompletedTask;
    }
}