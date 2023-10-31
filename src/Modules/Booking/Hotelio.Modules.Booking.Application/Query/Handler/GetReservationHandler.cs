using System;
using Hotelio.Shared.Queries;
using Hotelio.Modules.Booking.Application.ReadModel;
using MediatR;

namespace Hotelio.Modules.Booking.Application.Query.Handler;

internal class GetReservationHandler : IRequestHandler<GetReservation, Reservation>
{
    private readonly IReadModelStorage _storage;

    public GetReservationHandler(IReadModelStorage storage)
    {
        this._storage = storage;
    }

    public async Task<Reservation> Handle(GetReservation request, CancellationToken cancellationToken)
    {
        var query = request;
        var reservation = await this._storage.FindAsync(query.ReservationId);

        if (reservation is null)
        {
            return null;
        }

        return reservation;
    }
}


