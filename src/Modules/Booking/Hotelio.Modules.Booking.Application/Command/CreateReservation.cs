using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;

using System.ComponentModel.DataAnnotations;

public record CreateReservation(
    [Required] string Id,
    [Required] string HotelId,
    [Required] string OwnerId,
    [Range(1, int.MaxValue, ErrorMessage = "RoomType must be a positive integer.")]
    int RoomType,
    [Range(1, int.MaxValue, ErrorMessage = "NumberOfGuests must be a positive integer.")]
    int NumberOfGuests,
    [Range(0.01, double.MaxValue, ErrorMessage = "PriceToPay must be greater than 0.")]
    double PriceToPay,
    [Range(1, int.MaxValue, ErrorMessage = "PaymentType must be a positive integer.")]
    int PaymentType,
    [Range(typeof(DateTime), "2000-01-01", "9999-12-31", ErrorMessage = "Field StartDate is required")] 
    DateTime StartDate,
    [Range(typeof(DateTime), "2000-01-01", "9999-12-31", ErrorMessage = "Field EndDate is required")] 
    DateTime EndDate,
    List<string> Amenities
): ICommand;



