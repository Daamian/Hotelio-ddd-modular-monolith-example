using System;
using Hotelio.Shared.Queries;
using Hotelio.Modules.Booking.Application.ReadModel;

namespace Hotelio.Modules.Booking.Application.Query.Handler;

internal class GetReservationHandler : IQueryHandler<GetReservation, Reservation>
{
    private readonly IReadModelStorage storage;

    public GetReservationHandler(IReadModelStorage storage)
    {
        this.storage = storage;
    }

    public async Task<Reservation> HandleAsync(GetReservation query)
    {
        var reservation = await this.storage.FindAsync(query.ReservationId);

        return reservation;
    }
}


