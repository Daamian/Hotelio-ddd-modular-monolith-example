using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;

internal record RejectReservation(string ReservationId) : ICommand;
