using System;
using System.Runtime.CompilerServices;
using Hotelio.Modules.Booking.Application.ReadModel.VO;

namespace Hotelio.Modules.Booking.Application.ReadModel;

internal record Reservation(
    Guid Id, 
    Hotel Hotel, 
    Owner Owner, 
    RoomType RoomType, 
    int NumberOfGuests,
    string Status,
    double PriceToPay, 
    double PricePayed, 
    string PaymentType, 
    DateTime StartDate, 
    DateTime EndDate,
    List<Amenity> Amenities, 
    string? RoomId = null
    );




