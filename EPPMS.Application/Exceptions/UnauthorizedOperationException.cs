namespace EPPMS.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a user attempts
/// to perform an operation they are not permitted to perform.
/// </summary>
public sealed class UnauthorizedOperationException : EppmsException
{
    public UnauthorizedOperationException() : base("You are not authorized to perform this operation.")
    {
    }

    public UnauthorizedOperationException(string message) : base(message)
    {
    }

    public UnauthorizedOperationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}