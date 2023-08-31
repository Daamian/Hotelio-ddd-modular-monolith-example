using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;

internal record FinishReservation(string ReservationId) : ICommand;

