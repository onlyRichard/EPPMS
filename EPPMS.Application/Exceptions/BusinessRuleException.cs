namespace EPPMS.Application.Exceptions
{
    public abstract class BusinessRuleException : Exception
    {
        protected BusinessRuleException(
            int statusCode,
            string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        protected BusinessRuleException(
            int statusCode,
            string message,
            Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}