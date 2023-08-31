using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class StartReservationHandler : ICommandHandler<StartReservation>
{
    public async Task HandleAsync(StartReservation command)
    {
        // Logika obsługi komendy StartReservation
        await Task.CompletedTask;
    }
}


