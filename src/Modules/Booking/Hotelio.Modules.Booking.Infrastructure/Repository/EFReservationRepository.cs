using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Modules.Booking.Infrastructure.DAL;
using Hotelio.Shared.Event;

namespace Hotelio.Modules.Booking.Infrastructure.Repository;

internal class EFReservationRepository: IReservationRepository
{
    private ReservationDbContext _dbc;
    private readonly IEventBus _eventBus;

    public EFReservationRepository(ReservationDbContext dbc, IEventBus eventBus)
    {
        _dbc = dbc;
        _eventBus = eventBus;
    }
    
    public async Task AddAsync(Reservation reservation)
    {
        _dbc.Reservations.Add(reservation);
        await _dbc.SaveChangesAsync();
        var events = reservation.Events.Select(item => item).ToList();
        reservation.Events.Clear();
        await publishEvents(events);
    }

    #nullable enable
    public Reservation? Find(string id) => _dbc.Reservations.Find(id);
    
    public async Task<Reservation>? FindAsync(string id) => await _dbc.Reservations.FindAsync(id);

    public async Task UpdateAsync(Reservation reservation)
    {
        _dbc.Reservations.Attach(reservation);
        await _dbc.SaveChangesAsync();
        await publishEvents(reservation.Events);
    }
    
    private async Task publishEvents(List<IEvent> events)
    {
        foreach (var domainEvent in events)
        {
            await _eventBus.Publish(domainEvent);
        }
    }
}