namespace EPPMS.Application.Exceptions
{
    public sealed class UnauthorizedException : BusinessRuleException
    {
        public UnauthorizedException(string message)
            : base(401, message)
        {
        }

        public UnauthorizedException(
            string message,
            Exception innerException)
            : base(401, message, innerException)
        {
        }
    }
}