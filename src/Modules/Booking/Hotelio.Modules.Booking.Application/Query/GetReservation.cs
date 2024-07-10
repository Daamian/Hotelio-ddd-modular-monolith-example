using Hotelio.Shared.Queries;
using Hotelio.Modules.Booking.Application.ReadModel;
namespace Hotelio.Modules.Booking.Application.Query;

public class GetReservation: IQuery<Reservation>
{
    public Guid ReservationId { set; get; }

    public GetReservation(Guid reservationId)
    {
        ReservationId = reservationId;
    }
}


