﻿using System;
using Hotelio.Shared.Queries;
using Hotelio.Modules.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Domain.Repository;
using MediatR;

namespace Hotelio.Modules.Booking.Application.Query.Handler;

internal class GetReservationHandler : IRequestHandler<GetReservation, Reservation?>
{
    private readonly IReadModelStorage _storage;

    public GetReservationHandler(IReadModelStorage storage)
    {
        _storage = storage;
    }

    public async Task<Reservation?> Handle(GetReservation request, CancellationToken cancellationToken)
    {
        return await _storage.FindAsync(request.ReservationId);
    }
}


