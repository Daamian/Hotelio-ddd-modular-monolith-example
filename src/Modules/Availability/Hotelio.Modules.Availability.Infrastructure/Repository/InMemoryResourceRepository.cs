using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Modules.Availability.Infrastructure.Storage;

namespace Hotelio.Modules.Availability.Infrastructure.Repository;

internal class InMemoryResourceRepository : IResourceRepository
{
    public Resource? Find(Guid id)
    {
        var recordFound = InMemoryStorage.Resources.Find(r => r["Id"] == id.ToString());

        if (recordFound is null) {
            return null;
        }

        return Resource.Create(new Guid(recordFound["id"]), recordFound["group"], recordFound["type"]);
    }

    public async Task UpdateAsync(Resource resource)
    {
        var index = InMemoryStorage.Resources
            .FindIndex(r => r["Id"] == resource.Snapshot()["Id"]);

        if (index == -1)
        {
            throw new Exception($"Resource with id {resource.Snapshot()["Id"]} doesn't exists");
        }

        InMemoryStorage.Resources[index] = resource.Snapshot();
    }
}