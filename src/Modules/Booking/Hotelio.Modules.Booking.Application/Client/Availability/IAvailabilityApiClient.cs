namespace Hotelio.Modules.Booking.Application.Client.Availability;

public interface IAvailabilityApiClient
{
    Task Book(string hotelId, string roomType, string reservationId, DateTime startDate, DateTime endDate);
    Task UnBook(string roomId, string reservationId, DateTime startDate, DateTime endDate);
}