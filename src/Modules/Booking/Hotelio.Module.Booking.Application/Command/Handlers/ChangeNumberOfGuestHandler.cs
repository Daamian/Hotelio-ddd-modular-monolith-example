using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

internal sealed class ChangeNumberOfGuestsHandler : ICommandHandler<ChangeNumberOfGuests>
{
    public async Task HandleAsync(ChangeNumberOfGuests command)
    {
        // Logika obsługi komendy ChangeNumberOfGuests
        await Task.CompletedTask;
    }
}
