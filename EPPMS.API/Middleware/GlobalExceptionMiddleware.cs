using System.Text.Json;
using EPPMS.API.Exceptions;

namespace EPPMS.API.Middleware
{
    public sealed class GlobalExceptionMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        #endregion

        #region Constructor
        public GlobalExceptionMiddleware(RequestDelegate next,ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }
        #endregion

        #region Private Methods
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception,
                "Unhandled exception occurred. TraceId: {TraceId}, Method: {Method}, Path: {Path}",
                context.TraceIdentifier,
                context.Request.Method,
                context.Request.Path);

            var problemDetails = ProblemDetailsFactory.Create(exception,context.TraceIdentifier,context.Request.Path);
            context.Response.Clear();
            context.Response.StatusCode = problemDetails.Status!.Value;
            context.Response.ContentType = "application/problem+json";

            await JsonSerializer.SerializeAsync(context.Response.Body,problemDetails);
        }
        #endregion
    }
}