namespace Hotelio.Modules.Booking.Domain.Model.Snapshot;

internal record ReservationSnapshot(
    string Id, 
    string HotelId, 
    string OwnerId, 
    string? RoomId,
    int RoomType, 
    int NumberOfGuests, 
    int Status, 
    double PriceToPay, 
    double PricePayed, 
    int PaymentType, 
    DateTime StartDate,
    DateTime EndDate,
    List<Amenity> Amenities
    );