namespace Hotelio.Modules.HotelManagement.Core.Model;

internal class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<Room> Rooms { get; set; } = new();
}