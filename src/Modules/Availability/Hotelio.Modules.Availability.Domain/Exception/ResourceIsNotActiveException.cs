using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Availability.Domain.Exception;

internal class ResourceIsNotActiveException: DomainException
{
    public ResourceIsNotActiveException(string message) : base(message)
    {
    }
}