using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

internal sealed class ExtendReservationHandler : ICommandHandler<ExtendReservation>
{
    public async Task HandleAsync(ExtendReservation command)
    {
        // Logika obsługi komendy ExtendReservation
        await Task.CompletedTask;
    }
}
