namespace Hotelio.Modules.Booking.Infrastructure.Repository;

using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Infrastructure.Storage;

internal class InMemoryReservationRepository : IReservationRepository
{
    public async Task AddAsync(Reservation reservation)
    {
        InMemoryStorage.reservations.Add(reservation);
    }
}


