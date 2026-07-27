namespace EPPMS.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested operation
/// conflicts with the current state of the resource.
/// </summary>
public sealed class ConflictException : EppmsException
{
    public ConflictException() : base("The requested operation resulted in a conflict.")
    {
    }

    public ConflictException(string message) : base(message)
    {
    }

    public ConflictException(string message, Exception innerException) : base(message, innerException)
    {
    }
}