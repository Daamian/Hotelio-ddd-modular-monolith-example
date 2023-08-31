using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;

internal record ExtendReservation(string ReservationId, DateRange DateRange) : ICommand;

