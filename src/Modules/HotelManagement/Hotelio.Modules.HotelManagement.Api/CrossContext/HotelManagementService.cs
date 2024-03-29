using Hotelio.CrossContext.Contract.HotelManagement;
using Hotelio.CrossContext.Contract.HotelManagement.DTO;
using Hotelio.CrossContext.Contract.HotelManagement.Exception;

namespace Hotelio.Modules.HotelManagement.Api.CrossContext;

internal class HotelManagementService: IHotelManagement
{
    private readonly List<Hotel> _hotels = new List<Hotel>()
    {
        new Hotel("Hotel-1", new List<Amenity>() { new Amenity("amenity-1"), new Amenity("amenity-2") }, new List<RoomType> { new RoomType(1, 2, 1), new RoomType(2, 2, 2) }),
        new Hotel("Hotel-2", new List<Amenity>() { new Amenity("amenity-21"), new Amenity("amenity-22") }, new List<RoomType> { new RoomType(1, 2, 1), new RoomType(2, 2, 2) })
    };

    public async Task<Hotel> GetAsync(string id)
    {
        //TODO get from repository
        var hotel = this._hotels.Find(h => h.Id == id);

        if (null == hotel)
        {
            throw new HotelNotFoundException($"Not found hotel with id {id}");
        }

        return hotel;
    }


    public async Task<string> GetFirstAvailableRoom(string roomType, DateTime startDate, DateTime endDate)
    {
        return "room-1";
    }
}