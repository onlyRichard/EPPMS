using System.Collections.ObjectModel;

namespace EPPMS.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when one or more validation
/// errors occur during application processing.
/// </summary>
public sealed class ValidationException : EppmsException
{
    private static readonly IReadOnlyDictionary<string, string[]> EmptyErrors =  new ReadOnlyDictionary<string, string[]>(new Dictionary<string, string[]>());

    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public ValidationException() : base("One or more validation errors occurred.")
    {
        Errors = EmptyErrors;
    }

    public ValidationException(string message) : base(message)
    {
        Errors = EmptyErrors;
    }

    public ValidationException(IDictionary<string, string[]> errors) : base("One or more validation errors occurred.")
    {
        Errors = new ReadOnlyDictionary<string, string[]>(new Dictionary<string, string[]>(errors));
    }

    public ValidationException(string message,IDictionary<string, string[]> errors) : base(message)
    {
        Errors = new ReadOnlyDictionary<string, string[]>(new Dictionary<string, string[]>(errors));
    }

    public ValidationException(string message, Exception innerException) : base(message, innerException)
    {
        Errors = EmptyErrors;
    }
}