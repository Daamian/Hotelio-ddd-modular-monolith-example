using Hotelio.CrossContext.Contract.Availability;
using Hotelio.CrossContext.Contract.Booking;
using Hotelio.CrossContext.Contract.Catalog;
using Hotelio.CrossContext.Contract.Catalog.Exception;
using Hotelio.CrossContext.Contract.Shared.Exception;

namespace Hotelio.CrossContext.Saga.ReservationProcess;

public class ReservationProcessManager
{
    private readonly IAvailability _availability;
    private readonly ICatalogSearcher _catalog;
    private readonly IBooking _booking;

    public ReservationProcessManager(
        IAvailability availability,
        ICatalogSearcher catalogSearcher,
        IBooking booking
    )
    {
        _availability = availability;
        _catalog = catalogSearcher;
        _booking = booking;
    }

    public async Task TryToBookResource(string reservationId)
    {
        var reservation = await _booking.GetAsync(reservationId);

        if (null == reservation)
        {
            return;
        }

        try
        {
            var roomId = await _catalog.FindFirstRoomAvailableAsync(
                reservation.HotelId,
                reservation.RoomType,
                reservation.StartDate,
                reservation.EndDate
            );

            await _availability.BookAsync(roomId, reservation.Id, reservation.StartDate, reservation.EndDate);
        }
        catch (NotFoundAvailableRoomException e)
        {
            await _booking.RejectReservation(reservation.Id);
        }
    }

    public async Task ConfirmReservation(string resourceId, string reservationId)
    {
        await _booking.ConfirmReservation(resourceId, reservationId);
    }

    public async Task UnBookResource(string reservationId)
    {
        var reservation = await _booking.GetAsync(reservationId);
        if (reservation?.RoomId != null) {
            await _availability.UnBookAsync(reservation.RoomId, reservation.Id);
        }
        else
        {
            //TODO: what if ??
        }
    }

    public async Task RejectReservation(string reservationId)
    {
        await _booking.RejectReservation(reservationId);
    }
}