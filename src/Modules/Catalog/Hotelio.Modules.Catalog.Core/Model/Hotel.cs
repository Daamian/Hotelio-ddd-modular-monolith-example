using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotelio.Modules.Catalog.Core.Model;

internal class Hotel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Room> Rooms { get; set; } = new();
}