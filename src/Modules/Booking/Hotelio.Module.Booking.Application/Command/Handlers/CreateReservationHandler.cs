using System;
using Hotelio.Shared.Commands;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;

namespace Hotelio.Module.Booking.Application.Command.Handlers;

internal sealed class CreateReservationHandler : ICommandHandler<CreateReservation>
{
    private readonly IReservationRepository _reservationRepository;

    public CreateReservationHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task HandleAsync(CreateReservation command)
    {
        //TODO pobrać hotelConfig z kontekstu zarządzania
        //TODO Zablokować wcześniej dostępność
        //TODO przemapować amenities

        var reservation = Reservation.Create(
            command.Id,
            command.HotelId,
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


