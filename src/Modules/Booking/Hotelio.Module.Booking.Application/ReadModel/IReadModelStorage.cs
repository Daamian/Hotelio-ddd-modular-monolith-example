using System;
namespace Hotelio.Module.Booking.Application.ReadModel;

internal interface IReadModelStorage
{
    public Task<Reservation> FindAsync(Guid reservationId);
}


