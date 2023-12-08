using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Modules.Availability.Infrastructure.Storage;
using Hotelio.Shared.Event;

namespace Hotelio.Modules.Availability.Infrastructure.Repository;

internal class InMemoryResourceRepository : IResourceRepository
{
    private readonly IEventBus _eventBus;

    public InMemoryResourceRepository(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public Resource? Find(Guid id)
    {
        var recordFound = InMemoryStorage.Resources.Find(r => r["Id"] == id.ToString());

        if (recordFound is null) {
            return null;
        }

        return Resource.Create(new Guid(recordFound["Id"]), recordFound["GroupId"], recordFound["Type"]);
    }

    public async Task UpdateAsync(Resource resource)
    {
        var index = InMemoryStorage.Resources
            .FindIndex(r => r["Id"] == resource.Snapshot()["Id"].ToString());

        if (index == -1)
        {
            throw new Exception($"Resource with id {resource.Snapshot()["Id"]} doesn't exists");
        }

        InMemoryStorage.Resources[index] = resource.Snapshot();
        this.publishEvents(resource);
    }
    
    private void publishEvents(Resource resource)
    {
        var events = resource.Events.ToList();
        foreach (var domainEvent in events)
        {
            this._eventBus.publish(domainEvent);
        }
        
        resource.Events.Clear();
    }
}