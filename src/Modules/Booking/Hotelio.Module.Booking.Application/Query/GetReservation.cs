using System;
using Hotelio.Shared.Queries;
using Hotelio.Module.Booking.Application.ReadModel;
namespace Hotelio.Module.Booking.Application.Query;

public class GetReservation: IQuery<Reservation>
{
    public Guid ReservationId { set; get; }

    public GetReservation(Guid reservationId)
    {
        this.ReservationId = reservationId;
    }
}


