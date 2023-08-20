﻿using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

internal sealed class FinishReservationHandler : ICommandHandler<FinishReservation>
{
    public async Task HandleAsync(FinishReservation command)
    {
        // Logika obsługi komendy FinishReservation
        await Task.CompletedTask;
    }
}
