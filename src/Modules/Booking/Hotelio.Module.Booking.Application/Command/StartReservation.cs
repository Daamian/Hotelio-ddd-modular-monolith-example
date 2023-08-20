using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Domain.Model
{
    internal record StartReservation(string ReservationId) : ICommand;
}
