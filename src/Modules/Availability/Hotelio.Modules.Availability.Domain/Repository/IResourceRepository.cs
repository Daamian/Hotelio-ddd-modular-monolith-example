using Hotelio.Modules.Availability.Domain.Model;

namespace Hotelio.Modules.Availability.Domain.Repository;

internal interface IResourceRepository
{
    Task UpdateAsync(Resource resource);
    Task AddAsync(Resource resource);
    Task<Resource?> FindAsync(Guid id);
    Task<Resource?> FindByExternalIdAsync(string resourceId);
}