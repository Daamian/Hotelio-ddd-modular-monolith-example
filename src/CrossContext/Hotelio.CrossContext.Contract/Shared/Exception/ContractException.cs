namespace Hotelio.CrossContext.Contract.Shared.Exception;

public abstract class ContractException : System.Exception
{
    protected ContractException(string message) : base(message)
    {
        
    }
}