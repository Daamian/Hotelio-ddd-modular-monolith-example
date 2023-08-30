using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Domain.Model
{
    internal record ChangeNumberOfGuests(string ReservationId, int NumberOfGuests) : ICommand;
}
