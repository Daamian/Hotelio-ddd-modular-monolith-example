using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Domain.Model
{
    internal record ExtendReservation(string ReservationId, DateRange DateRange) : ICommand;
}
