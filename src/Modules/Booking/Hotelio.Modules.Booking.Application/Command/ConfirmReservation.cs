using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;

internal record ConfirmReservation(string ReservationId) : ICommand;

