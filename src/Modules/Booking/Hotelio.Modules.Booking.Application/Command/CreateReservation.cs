using System;
using System.Net.NetworkInformation;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;

public record CreateReservation(
    string Id,
    string HotelId,
    int RoomType,
    int NumberOfGuests,
    double PriceToPay,
    int PaymentType,
    DateTime StartDate,
    DateTime EndDate,
    List<string> Amenities
): ICommand;



