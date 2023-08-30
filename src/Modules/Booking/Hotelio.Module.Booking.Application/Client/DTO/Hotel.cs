using System;
namespace Hotelio.Module.Booking.Application.Client.DTO;

internal class Hotel
{
    public readonly string Id;
    public readonly List<Amenity> Amenities;
    public readonly List<RoomType> RoomTypes;

    public Hotel(string id, List<Amenity> amenities, List<RoomType> roomTypes)
    {
        this.Id = id;
        this.Amenities = amenities;
        this.RoomTypes = roomTypes;
    }
}


