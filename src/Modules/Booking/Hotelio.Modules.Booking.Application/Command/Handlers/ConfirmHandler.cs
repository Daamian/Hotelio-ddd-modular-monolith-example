using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class ConfirmReservationHandler : ICommandHandler<ConfirmReservation>
{
    public async Task HandleAsync(ConfirmReservation command)
    {
        // Logika obsługi komendy ConfirmReservation
        await Task.CompletedTask;
    }
}


