namespace Hotelio.Shared.Exception;

using System;

public class CommandFailedException : Exception
{
    public CommandFailedException(string message) : base(message)
    {
    }
}