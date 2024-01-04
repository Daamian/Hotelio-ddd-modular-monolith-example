using Hotelio.Modules.Availability.Application.ReadModel;
using Hotelio.Modules.Availability.Infrastructure.Storage;

namespace Hotelio.Modules.Availability.Infrastructure.ReadModel;

internal class InMemoryResourceStorage: IResourceStorage
{
    public Resource FindFirstAvailableInDates(string groupId, int type, DateTime startDate, DateTime endDate)
    {
        //TODO query
        /*var data = InMemoryStorage.Resources
            .Where(resource =>
                resource["GroupId"] == groupId &&
                resource["Type"] == type &&
                ((List<IDictionary<string, dynamic>>)resource["Books"])
                .Any(book =>
                    book["StartDate"] == startDate))
            .ToList().First();*/
        
        //TODO change linqu query
        /*var data = from resource in InMemoryStorage.Resources
            where resource.ContainsKey("GroupId") && resource["GroupId"] == groupId
                                                  && resource.ContainsKey("Type") && resource["Type"] == type
                                                  && resource.ContainsKey("Books")
                                                  && ((List<IDictionary<string, dynamic>>)resource["Books"])
                                                  .Any(book => book.ContainsKey("Id") && book["Id"] == groupId)
            select resource.ToList().First();*/
        
        var data = InMemoryStorage.Resources
            .Where(resource =>
                resource["GroupId"] == groupId &&
                resource["Type"] == type)
            .ToList().First();

        return new Resource(data["Id"], data["GroupId"], data["Type"], data["IsActive"], new List<Book>());
    }
}