using System;
using Hotelio.Shared.Commands;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Module.Booking.Application.Client;
using Hotelio.Modules.Booking.Domain.Model.DTO;

namespace Hotelio.Module.Booking.Application.Command.Handlers;

internal sealed class CreateReservationHandler : ICommandHandler<CreateReservation>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IHotelApiClient _hotelApiClient;

    public CreateReservationHandler(IReservationRepository reservationRepository, IHotelApiClient hotelApiClient)
    {
        _reservationRepository = reservationRepository;
        _hotelApiClient = hotelApiClient;
    }

    public async Task HandleAsync(CreateReservation command)
    {
        var hotel = await this._hotelApiClient.GetAsync(command.HotelId);

        var reservation = Reservation.Create(
            command.Id,
            new HotelConfig(
                hotel.Id,
                hotel.Amenities.Select(a => a.Id).ToList(),
                hotel.RoomTypes.Select(r => new RoomTypeConfig(r.Id, r.MaxGuests, r.Level)).ToList()
            ),
            command.RoomType,
            command.NumberOfGuests,
            command.PriceToPay,
            (PaymentType) command.PaymentType,
            new DateRange(command.StartDate, command.EndDate),
            command.Amenities.Select(id => new Amenity(id)).ToList()
        );

        await _reservationRepository.AddAsync(reservation);
        await Task.CompletedTask;
    }
}


