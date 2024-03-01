using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Shared.Event;
using Microsoft.EntityFrameworkCore;

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

    public Resource? Find(Guid id) => _dbContext.Resources
        .Include(r => r.Books)
        .FirstOrDefault(r => r.Id == id);

    public async Task UpdateAsync(Resource resource)
    {
        _dbContext.Resources.Attach(resource);
        await _dbContext.SaveChangesAsync();
        await this.publishEvents(resource);
    }

    public async Task AddAsync(Resource resource)
    {
        await _dbContext.Resources.AddAsync(resource);
        await _dbContext.SaveChangesAsync();
    }

    public void Add(Resource resource)
    {
        _dbContext.Resources.Add(resource);
        _dbContext.SaveChanges();
    }

    public async Task<Resource?> FindAsync(Guid id) => await _dbContext.Resources.FindAsync(id);
    public void Update(Resource resource)
    {
        //var resourceFound = _dbContext.Resources.FirstOrDefault(u => u.Id == resource.Id);

        _dbContext.Update(resource);
        _dbContext.SaveChanges();
        //this.publishEvents(resource);
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