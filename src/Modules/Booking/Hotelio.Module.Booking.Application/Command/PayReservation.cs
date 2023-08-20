using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Domain.Model
{
    internal record PayReservation(string ReservationId, double Price) : ICommand;
}
