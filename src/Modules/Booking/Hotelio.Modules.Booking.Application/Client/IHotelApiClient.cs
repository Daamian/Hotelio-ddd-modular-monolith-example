
using Hotelio.Modules.Booking.Application.Client.DTO;
namespace Hotelio.Modules.Booking.Application.Client;

internal interface IHotelApiClient
{
    Task<Hotel> GetAsync(string id);
    Task<string> GetFirstAvailableRoom(string roomType, DateTime startDate, DateTime endDate);
}


