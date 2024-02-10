using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Shared.Event;

namespace Hotelio.Modules.Availability.Infrastructure.Repository;

internal class EFResourceRepository: IResourceRepository
{
    private ResourceDbContext _dbContext;
    private readonly IEventBus _eventBus;

    public EFResourceRepository(ResourceDbContext dbContext, IEventBus eventBus)
    {
        _dbContext = dbContext;
        _eventBus = eventBus;
    }

    public Resource? Find(Guid id) => _dbContext.Resources.Find(id);

    public async Task UpdateAsync(Resource resource)
    {
        _dbContext.Resources.Attach(resource);
        await _dbContext.SaveChangesAsync();
        await this.publishEvents(resource);
    }
    
    private async Task publishEvents(Resource resource)
    {
        var events = resource.Events.ToList();
        foreach (var domainEvent in events)
        {
            await this._eventBus.publish(domainEvent);
        }
        
        resource.Events.Clear();
    }
}