namespace Hotelio.CrossContext.Contract.Booking.DTO;

public record Reservation(string Id, string HotelId, string RoomType, DateTime StartDate, DateTime EndDate, bool IsPostPaid, bool IsInAdvance, string? RoomId);