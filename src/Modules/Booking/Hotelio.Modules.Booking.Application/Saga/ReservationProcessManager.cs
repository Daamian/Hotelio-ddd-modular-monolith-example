namespace Hotelio.Modules.Booking.Application.Saga;

public class ReservationProcessManager
{
    public void HandleReservationCreated()
    {
        //TODO book resource
    }

    public void HandleResourceBooked()
    {
        //TODO confirm reservation
        //TODO consider what if confirmation is not possible
    }

    public void HandleResourceBookRejected()
    {
        //TODO reject reservation
    }

    public void HandleReservationCanceled()
    {
        //TODO unbook resource
        //TODO consider what if unbook is not possible
    }
}