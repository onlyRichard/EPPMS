namespace EPPMS.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a business rule is violated.
/// </summary>
public sealed class BusinessRuleException : EppmsException
{
    public BusinessRuleException() : base("A business rule has been violated.")
    {
    }

    public BusinessRuleException(string message) : base(message)
    {
    }

    public BusinessRuleException(string message, Exception innerException) : base(message, innerException)
    {
    }
}