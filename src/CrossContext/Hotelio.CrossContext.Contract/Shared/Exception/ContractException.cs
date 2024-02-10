namespace Hotelio.CrossContext.Contract.Shared.Exception;

public abstract class ContractException : System.Exception
{
    public ContractException(string message) : base(message)
    {
        
    }
}