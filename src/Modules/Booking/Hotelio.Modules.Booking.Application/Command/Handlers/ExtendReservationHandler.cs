using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class ExtendReservationHandler
{
    public async Task HandleAsync(ExtendReservation command)
    {
        // Logika obsługi komendy ExtendReservation
        await Task.CompletedTask;
    }
}
