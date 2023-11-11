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
        
        foreach (var domainEvent in reservation.Events)
        {
            this._eventBus.publish(domainEvent);
        }
    }
}


