using Hotelio.Modules.Availability.Domain.Model;

namespace Hotelio.Modules.Availability.Domain.Repository;

internal interface IResourceRepository
{
    Resource? Find(Guid id);
    Task UpdateAsync(Resource resource);
}