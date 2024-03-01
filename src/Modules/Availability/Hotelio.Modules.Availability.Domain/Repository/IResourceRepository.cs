using Hotelio.Modules.Availability.Domain.Model;

namespace Hotelio.Modules.Availability.Domain.Repository;

internal interface IResourceRepository
{
    Resource? Find(Guid id);
    Task UpdateAsync(Resource resource);
    Task AddAsync(Resource resource);
    void Add(Resource resource);
    Task<Resource?> FindAsync(Guid id);
    void Update(Resource resource);
}