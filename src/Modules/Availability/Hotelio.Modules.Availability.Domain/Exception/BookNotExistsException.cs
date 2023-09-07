using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Availability.Domain.Exception;

public class BookNotExistsException : DomainException
{
    public BookNotExistsException(string message) : base(message)
    {
    }
}