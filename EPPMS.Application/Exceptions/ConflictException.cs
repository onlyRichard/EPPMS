namespace EPPMS.Application.Exceptions
{
    public sealed class ConflictException : BusinessRuleException
    {
        public ConflictException(string message)
            : base(409, message)
        {
        }

        public ConflictException(
            string message,
            Exception innerException)
            : base(409, message, innerException)
        {
        }
    }
}