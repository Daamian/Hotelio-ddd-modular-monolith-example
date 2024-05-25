namespace Hotelio.Modules.Catalog.Core.Model;

internal class Hotel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Room> Rooms { get; set; } = new List<Room>();
}