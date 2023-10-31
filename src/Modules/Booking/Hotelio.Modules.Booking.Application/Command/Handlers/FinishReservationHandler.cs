using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class FinishReservationHandler
{
    public async Task HandleAsync(FinishReservation command)
    {
        // Logika obsługi komendy FinishReservation
        await Task.CompletedTask;
    }
}
