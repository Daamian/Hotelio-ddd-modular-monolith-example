namespace Hotelio.Modules.Booking.Application.Client.Availability;

internal interface IAvailabilityApiClient
{
    Task Book(string reservationId, DateTime startDate, DateTime endDate);
    Task UnBook(string reservationId, DateTime startDate, DateTime endDate);
}