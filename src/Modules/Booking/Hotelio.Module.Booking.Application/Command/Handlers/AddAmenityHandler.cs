using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

internal sealed class AddAmenityToReservationHandler : ICommandHandler<AddAmenity>
{
    public async Task HandleAsync(AddAmenity command)
    {
        // Logika obsługi komendy AddAmenityToReservation
        await Task.CompletedTask;
    }
}
