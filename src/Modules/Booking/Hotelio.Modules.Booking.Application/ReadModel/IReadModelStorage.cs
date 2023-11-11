using System;
namespace Hotelio.Modules.Booking.Application.ReadModel;

internal interface IReadModelStorage
{
    #nullable enable
    public Task<Reservation>? FindAsync(Guid reservationId);
}


