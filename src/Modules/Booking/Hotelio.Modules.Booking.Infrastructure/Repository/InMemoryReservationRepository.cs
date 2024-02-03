using System;
using System.Collections.Generic;
using System.Linq;
using Hotelio.Shared.Event;

namespace Hotelio.Modules.Booking.Infrastructure.Repository;

using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Infrastructure.Storage;

internal class InMemoryReservationRepository : IReservationRepository
{
    private readonly IEventBus _eventBus;

    public InMemoryReservationRepository(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task AddAsync(Reservation reservation)
    {
        InMemoryStorage.Reservations.Add(reservation);
        this.publishEvents(reservation);
    }

    private void publishEvents(Reservation reservation)
    {
        var events = reservation.Events.ToList();
        foreach (var domainEvent in events)
        {
            this._eventBus.publish(domainEvent);
        }
        
        reservation.Events.Clear();
    }

    #nullable enable
    public Reservation? Find(string id)
    {
        return InMemoryStorage.Reservations.Find(r => (string) r.Snapshot()["Id"] == id);
    }

    public Task<Reservation>? FindAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Reservation reservation)
    {
        var index = InMemoryStorage.Reservations
            .FindIndex(r => r.Snapshot()["Id"] == reservation.Snapshot()["Id"]);

        if (index == -1)
        {
            throw new Exception($"Reservation with id {reservation.Snapshot()["Id"]} doesn't exists");
        }

        InMemoryStorage.Reservations[index] = reservation;
        this.publishEvents(reservation);
    }
}


