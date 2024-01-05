using Hotelio.CrossContext.Contract.HotelManagement.DTO;

namespace Hotelio.CrossContext.Contract.HotelManagement;

public interface IHotelManagement
{
    Task<Hotel> GetAsync(string id);
}