using Hotelio.CrossContext.Contract.HotelManagement;
using Hotelio.Modules.Booking.Application.ReadModel.VO;
using Hotelio.Modules.Booking.Domain.Event;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using MediatR;
using AmenityReadModel = Hotelio.Modules.Booking.Application.ReadModel.VO.Amenity;

namespace Hotelio.Modules.Booking.Application.ReadModel.Projector;

internal class ReservationReadModelProjector: INotificationHandler<ReservationCreated>, INotificationHandler<ReservationConfirmed>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IReadModelStorage _readModelStorage;
    private readonly IHotelManagement _hotelManagement;

    public ReservationReadModelProjector(
        IReservationRepository reservationRepository, 
        IReadModelStorage readModelStorage,
        IHotelManagement hotelManagement
        ) {
        _reservationRepository = reservationRepository;
        _readModelStorage = readModelStorage;
        _hotelManagement = hotelManagement;
    }

    public async Task Handle(ReservationCreated domainEvent, CancellationToken cancelationToken)
    {
        await _buildModel(domainEvent.Id);
    }
    
    public async Task Handle(ReservationConfirmed domainEvent, CancellationToken cancellationToken)
    {
        await _buildModel(domainEvent.ReservationId);
    }

    private async Task _buildModel(string reservationId)
    {
        var reservation = await _reservationRepository.FindAsync(reservationId);

        if (null == reservation)
        {
            return;
        }
        
        var hotel = await _hotelManagement.GetAsync(reservation.HotelId);

        var roomType = hotel.RoomTypes.Find(r => r.Id == reservation.RoomType);
        var amenities = reservation.Amenities.Select(a => hotel.Amenities.Find(ha => ha.Id == a.Id)).ToList();
        
        if (roomType is null)
        {
            return;
        }
        
        var model = new Reservation(
            reservationId,
            new Hotel(reservation.HotelId, hotel.Name),
            new Owner(reservation.OwnerId, "Damian", "Kusek"), //TODO get name from customer context
            new RoomType(roomType.Id, roomType.Name),
            reservation.NumberOfGuests,
            ((Status) reservation.Status).ToString(),
            reservation.PriceToPay,
            reservation.PricePayed,
            ((PaymentType) reservation.PaymentType).ToString(),
            reservation.DateRange.StartDate.ToLocalTime(), //TODO timestamp ???
            reservation.DateRange.EndDate.ToLocalTime(),
            amenities.Select(a => new AmenityReadModel(a.Id, a.Name)).ToList(),
            reservation.RoomId
        );

        await _readModelStorage.SaveAsync(model);
    }
}