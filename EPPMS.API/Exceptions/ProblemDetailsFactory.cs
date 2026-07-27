using EPPMS.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EPPMS.API.Exceptions
{
    public static class ProblemDetailsFactory
    {
        #region Public Methods

        public static EppmsProblemDetails Create(
            Exception exception,
            string traceId,
            string instance)
        {
            return exception switch
            {
                ValidationException validationException => CreateValidationProblemDetails(validationException, traceId, instance),
                NotFoundException notFoundException => CreateProblemDetails(StatusCodes.Status404NotFound,"Resource Not Found",notFoundException.Message,traceId,instance),
                ConflictException conflictException => CreateProblemDetails(StatusCodes.Status409Conflict,"Conflict",conflictException.Message,traceId,instance),
                UnauthorizedOperationException unauthorizedException => CreateProblemDetails(StatusCodes.Status403Forbidden,"Forbidden",unauthorizedException.Message,traceId,instance),
                BusinessRuleException businessRuleException => CreateProblemDetails(StatusCodes.Status422UnprocessableEntity, "Business Rule Violation",businessRuleException.Message,traceId,instance),
                _ => CreateProblemDetails(StatusCodes.Status500InternalServerError,"Internal Server Error","An unexpected error occurred.",traceId,instance)
            };
        }

        #endregion

        #region Private Methods
        private static EppmsProblemDetails CreateProblemDetails(
            int status,
            string title,
            string detail,
            string traceId,
            string instance)
        {
            return new EppmsProblemDetails
            {
                Type = $"https://httpstatuses.com/{status}",
                Title = title,
                Status = status,
                Detail = detail,
                Instance = instance,
                TraceId = traceId
            };
        }

        private static EppmsProblemDetails CreateValidationProblemDetails(ValidationException exception, string traceId, string instance)
        {
            return new EppmsProblemDetails
            {
                Type = $"https://httpstatuses.com/{StatusCodes.Status400BadRequest}",
                Title = "Validation Failed",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message,
                Instance = instance,
                TraceId = traceId,
                Errors = exception.Errors
            };
        }
        #endregion
    }
}