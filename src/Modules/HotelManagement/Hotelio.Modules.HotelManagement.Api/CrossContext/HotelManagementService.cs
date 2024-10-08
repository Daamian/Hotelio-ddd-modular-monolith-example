using Hotelio.CrossContext.Contract.HotelManagement;
using Hotelio.CrossContext.Contract.HotelManagement.DTO;
using Hotelio.CrossContext.Contract.HotelManagement.Exception;
using Hotelio.Modules.HotelManagement.Core.Service;
using Microsoft.IdentityModel.Tokens;

namespace Hotelio.Modules.HotelManagement.Api.CrossContext;

internal class HotelManagementService: IHotelManagement
{
    private readonly IHotelService _service;

    public HotelManagementService(IHotelService service) => _service = service;

    public async Task<Hotel> GetAsync(string id)
    {
        var hotel = await _service.GetAsync(int.Parse(id));

        if (null == hotel)
        {
            throw new HotelNotFoundException($"Not found hotel with id {id}");
        }
        
        return new Hotel(
            hotel.Id.ToString(),
            hotel.Name,
            hotel.Amenities.IsNullOrEmpty()
                ? new List<Amenity>()
                : hotel.Amenities.Select(amenity => new Amenity(amenity.Id.ToString(), amenity.Name)).ToList(),
            hotel.Rooms.IsNullOrEmpty()
                ? new List<RoomType>()
                : hotel.Rooms.Select(room => new RoomType(
                    room.Type, 
                    room.TypeDetails.Name,
                    room.TypeDetails.MaxGuests, 
                    room.TypeDetails.Level)
                ).ToList());
    }
}