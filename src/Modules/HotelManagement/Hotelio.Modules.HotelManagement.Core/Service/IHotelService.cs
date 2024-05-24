using Hotelio.Modules.HotelManagement.Core.Service.DTO;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal interface IHotelService
{
    public int Add(HotelDto dto);
    public void Update(HotelDto dto);
    public HotelDto? Get(int id);
}