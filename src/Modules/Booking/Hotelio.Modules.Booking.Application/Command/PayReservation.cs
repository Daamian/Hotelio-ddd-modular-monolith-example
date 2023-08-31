using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;

internal record PayReservation(string ReservationId, double Price) : ICommand;

