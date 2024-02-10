using Hotelio.CrossContext.Contract.Shared.Exception;

namespace Hotelio.CrossContext.Contract.Availability.Exception;

public class ResourceIsNotAvailableException : ContractException
{
    public ResourceIsNotAvailableException(string message) : base(message)
    {
        
    }
}