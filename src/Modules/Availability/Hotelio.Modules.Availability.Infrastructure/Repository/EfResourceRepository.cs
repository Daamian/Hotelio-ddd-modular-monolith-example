using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Shared.Event;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.Availability.Infrastructure.Repository;

internal class EfResourceRepository: IResourceRepository
{
    private readonly ResourceDbContext _dbContext;
    private readonly IEventBus _eventBus;

    public EfResourceRepository(ResourceDbContext dbContext, IEventBus eventBus)
    {
        _dbContext = dbContext;
        _eventBus = eventBus;
    }

    public Resource? Find(Guid id) => _dbContext.Resources
        .Include(r => r.Books)
        .FirstOrDefault(r => r.Id == id);

    public async Task UpdateAsync(Resource resource)
    {
        _dbContext.Resources.Attach(resource);
        await _dbContext.SaveChangesAsync();
        await _publishEvents(resource);
    }

    public async Task AddAsync(Resource resource)
    {
        await _dbContext.Resources.AddAsync(resource);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Resource?> FindAsync(Guid id) => await _dbContext.Resources.FindAsync(id);
    
    public async Task<Resource?> FindByExternalIdAsync(string resourceId)
    => await _dbContext.Resources.Where(r => r.ExternalId == resourceId).FirstOrDefaultAsync();
    

    private async Task _publishEvents(Resource resource)
    {
        var events = resource.Events.ToList();
        foreach (var domainEvent in events)
        {
            await this._eventBus.Publish(domainEvent);
        }
        
        resource.Events.Clear();
    }
}