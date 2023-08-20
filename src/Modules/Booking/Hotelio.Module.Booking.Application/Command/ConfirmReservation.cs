using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Domain.Model
{
    internal record ConfirmReservation(string ReservationId) : ICommand;
}
