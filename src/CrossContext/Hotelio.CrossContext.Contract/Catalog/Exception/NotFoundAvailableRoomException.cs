using Hotelio.CrossContext.Contract.Shared.Exception;

namespace Hotelio.CrossContext.Contract.Catalog.Exception;

public class NotFoundAvailableRoomException: ContractException
{
    public NotFoundAvailableRoomException(string message) : base(message)
    {
        
    }
}