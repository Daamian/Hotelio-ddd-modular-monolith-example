using System;
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
        var result = await _dbc.SaveChangesAsync();
        await publishEvents(reservation);
    }

    #nullable enable
    public Reservation? Find(string id) => _dbc.Reservations.Find(id);
    
    public async Task<Reservation>? FindAsync(string id) => await _dbc.Reservations.FindAsync(id);

    public async Task UpdateAsync(Reservation reservation)
    {
        _dbc.Reservations.Attach(reservation);
        await _dbc.SaveChangesAsync();
        await publishEvents(reservation);
    }
    
    private async Task publishEvents(Reservation reservation)
    {
        var events = reservation.Events.ToList();
        foreach (var domainEvent in events)
        {
            await this._eventBus.Publish(domainEvent);
        }
        
        reservation.Events.Clear();
    }
}