using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class AddAmenityToReservationHandler
{
    public async Task HandleAsync(AddAmenity command)
    {
        // Logika obsługi komendy AddAmenityToReservation
        await Task.CompletedTask;
    }
}
