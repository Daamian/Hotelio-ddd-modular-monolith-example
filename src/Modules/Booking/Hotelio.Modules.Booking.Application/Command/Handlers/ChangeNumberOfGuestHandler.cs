using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class ChangeNumberOfGuestsHandler : ICommandHandler<ChangeNumberOfGuests>
{
    public async Task HandleAsync(ChangeNumberOfGuests command)
    {
        // Logika obsługi komendy ChangeNumberOfGuests
        await Task.CompletedTask;
    }
}
