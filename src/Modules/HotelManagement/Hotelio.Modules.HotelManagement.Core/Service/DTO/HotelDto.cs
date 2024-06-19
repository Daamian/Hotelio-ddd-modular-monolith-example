namespace Hotelio.Modules.HotelManagement.Core.Service.DTO;

internal record HotelDto(int Id, string Name, List<int>? Amenities = null)
{
    public virtual bool Equals(HotelDto? hotelDto)
    {
        if (hotelDto == null)
        {
            return false;
        }

        return Id == hotelDto.Id &&
               Name == hotelDto.Name &&
               (Amenities == hotelDto.Amenities || (Amenities != null && hotelDto.Amenities != null && Amenities.SequenceEqual(hotelDto.Amenities)));
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Amenities);
    }
}; 