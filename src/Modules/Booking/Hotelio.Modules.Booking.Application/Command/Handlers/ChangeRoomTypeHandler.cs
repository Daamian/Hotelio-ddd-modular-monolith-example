using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class ChangeRoomTypeHandler : ICommandHandler<ChangeRoomType>
{
    public async Task HandleAsync(ChangeRoomType command)
    {
        // Logika obsługi komendy ChangeRoomType
        await Task.CompletedTask;
    }
}
