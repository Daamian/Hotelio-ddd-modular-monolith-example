using System;
using Hotelio.Modules.Booking.Domain.Model;

namespace Hotelio.Modules.Booking.Domain.Repository;

internal interface IReservationRepository
{
    Task AddAsync(Reservation reservation);
    
    Reservation? Find(string id);

    Task<Reservation>? FindAsync(string id);

    Task UpdateAsync(Reservation reservation);
}


