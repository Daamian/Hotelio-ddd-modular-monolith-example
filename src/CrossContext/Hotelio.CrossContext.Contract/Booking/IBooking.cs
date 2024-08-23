using Hotelio.CrossContext.Contract.Booking.DTO;

namespace Hotelio.CrossContext.Contract.Booking;

public interface IBooking
{
    Task<Reservation?> GetAsync(string id);

    Task RejectReservation(string id);
    Task ConfirmReservation(string roomId, string reservationId);
}