﻿namespace Hotelio.Modules.Booking.Application.ReadModel;

internal interface IReadModelStorage
{
    public Task<Reservation?> FindAsync(Guid reservationId);
    public Task SaveAsync(Reservation reservation);
}


