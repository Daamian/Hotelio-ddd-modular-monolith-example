using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;

internal record StartReservation(string ReservationId) : ICommand;

