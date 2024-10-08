﻿
using Hotelio.CrossContext.Contract.HotelManagement;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Modules.Booking.Domain.Model.DTO;
using MediatR;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class CreateReservationHandler : IRequestHandler<CreateReservation>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IHotelManagement _hotelManagement;

    public CreateReservationHandler(IReservationRepository reservationRepository, IHotelManagement hotelManagement)
    {
        _reservationRepository = reservationRepository;
        _hotelManagement = hotelManagement;
    }

    public async Task Handle(CreateReservation command, CancellationToken cancellationToken)
    {
        var hotel = await _hotelManagement.GetAsync(command.HotelId);

        var reservation = Reservation.Create(
            command.Id,
            new HotelConfig(
                hotel.Id,
                hotel.Amenities.Select(a => a.Id).ToList(),
                hotel.RoomTypes.Select(r => new RoomTypeConfig(r.Id, r.MaxGuests, r.Level)).ToList()
            ),
            command.OwnerId,
            command.RoomType,
            command.NumberOfGuests,
            command.PriceToPay,
            (PaymentType) command.PaymentType,
            new DateRange(command.StartDate, command.EndDate),
            command.Amenities.Select(id => new Amenity(id)).ToList()
        );

        await _reservationRepository.AddAsync(reservation);
    }
}


