using Hotelio.Modules.HotelManagement.Core.Model;

namespace Hotelio.Modules.HotelManagement.Core.Repository;

internal interface IHotelRepository
{
    public void Add(Hotel hotel);
    public Hotel? Find(int id);
    public void Update(Hotel hotel);
}