using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Domain.Model
{
    internal record CancelReservation(string ReservationId) : ICommand;
}
