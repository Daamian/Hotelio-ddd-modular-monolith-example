using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;

internal record ChangeNumberOfGuests(string ReservationId, int NumberOfGuests) : ICommand;

