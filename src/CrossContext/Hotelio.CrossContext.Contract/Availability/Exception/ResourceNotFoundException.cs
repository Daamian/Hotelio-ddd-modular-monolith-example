using Hotelio.CrossContext.Contract.Shared.Exception;

namespace Hotelio.CrossContext.Contract.Availability.Exception;

public class ResourceNotFoundException: ContractException
{
    public ResourceNotFoundException(string message) : base(message)
    {
        
    }
}