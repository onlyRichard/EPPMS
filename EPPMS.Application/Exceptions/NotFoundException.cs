namespace EPPMS.Application.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a requested resource cannot be found.
    /// </summary>
    public sealed class NotFoundException : EppmsException
    {
        public NotFoundException() : base("The requested resource was not found.")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
