using Hotelio.Modules.Booking.Application.ReadModel.VO;
using Hotelio.Modules.Booking.Domain.Event;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using MediatR;
using AmenityReadModel = Hotelio.Modules.Booking.Application.ReadModel.VO.Amenity;

namespace Hotelio.Modules.Booking.Application.ReadModel.Projector;

internal class ReservationReadModelProjector: INotificationHandler<ReservationCreated>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IReadModelStorage _readModelStorage;

    public ReservationReadModelProjector(
        IReservationRepository reservationRepository, 
        IReadModelStorage readModelStorage
        ) {
        _reservationRepository = reservationRepository;
        _readModelStorage = readModelStorage;
    }

    public async Task Handle(ReservationCreated domainEvent, CancellationToken cancelationToken)
    {
        this._buildModel(domainEvent.Id);
    }

    private void _buildModel(string reservationId)
    {
        var reservation = _reservationRepository.FindAsync(reservationId).Result;

        if (null == reservation)
        {
            return;
        }

        var snapshot = reservation.Snap();
        
        var model = new Reservation(
            reservationId,
            new Hotel(snapshot.HotelId, "Hotel name"), //TODO get hotel name from Hotel Management context
            new Owner(snapshot.OwnerId, "Damian", "Kusek"), //TODO get name from customer context
            new RoomType(snapshot.RoomType, "Superior"), //TODO get name from Hotel Management context
            snapshot.NumberOfGuests,
            ((Status) snapshot.Status).ToString(),
            snapshot.PriceToPay,
            snapshot.PricePayed,
            ((PaymentType) snapshot.PaymentType).ToString(),
            snapshot.StartDate.ToLocalTime(), //TODO timestamp ???
            snapshot.EndDate.ToLocalTime(),
            snapshot.Amenities.Select(a => new AmenityReadModel(a.Id, "All inc")).ToList(),
            snapshot.RoomId
        );

        _readModelStorage.SaveAsync(model);
    }
}