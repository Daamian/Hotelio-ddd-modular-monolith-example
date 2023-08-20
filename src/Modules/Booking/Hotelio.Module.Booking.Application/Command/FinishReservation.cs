using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Domain.Model
{
    internal record FinishReservation(string ReservationId) : ICommand;
}
