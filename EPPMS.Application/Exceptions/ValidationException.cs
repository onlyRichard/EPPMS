namespace EPPMS.Application.Exceptions
{
    public sealed class ValidationException : BusinessRuleException
    {
        public ValidationException(string message)
            : base(400, message)
        {
        }

        public ValidationException(
            string message,
            Exception innerException)
            : base(400, message, innerException)
        {
        }
    }
}