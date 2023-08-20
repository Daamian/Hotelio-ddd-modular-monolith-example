using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

internal sealed class CancelReservationHandler : ICommandHandler<CancelReservation>
{
    public async Task HandleAsync(CancelReservation command)
    {
        // Logika obsługi komendy CancelReservation
        await Task.CompletedTask;
    }
}
