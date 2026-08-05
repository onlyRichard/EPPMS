namespace EPPMS.Application.Exceptions
{
    public sealed class ForbiddenException : BusinessRuleException
    {
        public ForbiddenException(string message)
            : base(403, message)
        {
        }

        public ForbiddenException(
            string message,
            Exception innerException)
            : base(403, message, innerException)
        {
        }
    }
}