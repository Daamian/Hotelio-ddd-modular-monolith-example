using System;
using Hotelio.Module.Booking.Application.Client.DTO;
using Hotelio.Module.Booking.Application.Client.Exception;

namespace Hotelio.Module.Booking.Application.Client;

internal class HotelApiClient : IHotelApiClient
{
    private readonly List<Hotel> hotels = new List<Hotel>()
    {
        new Hotel("Hotel-1", new List<Amenity>() { new Amenity("amenity-1"), new Amenity("amenity-2") }, new List<RoomType> { new RoomType(1, 2, 1), new RoomType(2, 2, 2) }),
        new Hotel("Hotel-2", new List<Amenity>() { new Amenity("amenity-21"), new Amenity("amenity-22") }, new List<RoomType> { new RoomType(1, 2, 1), new RoomType(2, 2, 2) })

    };

    public async Task<Hotel> GetAsync(string id)
    {
        var hotel = this.hotels.Find(h => h.Id == id);

        if (null == hotel)
        {
            throw new HotelNotFoundException($"Not found hotel with id {id}");
        }

        return hotel;
    }
}


