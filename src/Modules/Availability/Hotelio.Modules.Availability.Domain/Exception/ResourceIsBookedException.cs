using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Availability.Domain.Exception;

internal class ResourceIsBookedException: DomainException
{
    public ResourceIsBookedException(string message) : base(message)
    {
    }
}