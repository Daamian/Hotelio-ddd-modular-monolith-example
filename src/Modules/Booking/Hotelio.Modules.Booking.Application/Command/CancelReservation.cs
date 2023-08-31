using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;

internal record CancelReservation(string ReservationId) : ICommand;

