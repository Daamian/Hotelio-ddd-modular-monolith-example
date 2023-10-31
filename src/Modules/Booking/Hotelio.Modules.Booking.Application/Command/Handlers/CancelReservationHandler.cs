using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class CancelReservationHandler
{
    public async Task HandleAsync(CancelReservation command)
    {
        // Logika obsługi komendy CancelReservation
        await Task.CompletedTask;
    }
}
