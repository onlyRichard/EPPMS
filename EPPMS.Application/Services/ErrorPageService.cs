using EPPMS.Application.DTOs.Error;
using EPPMS.Application.Interfaces.Services;

namespace EPPMS.Application.Services
{
    /// <summary>
    /// Provides enterprise error page information
    /// based on the supplied HTTP status code.
    /// </summary>
    public sealed class ErrorPageService : IErrorPageService
    {
        private sealed record ErrorDefinition(
            int StatusCode,
            string Title,
            string Message,
            string? Description,
            bool ShowGoBack,
            bool ShowDashboardButton);

        private static readonly IReadOnlyDictionary<int, ErrorDefinition> ErrorDefinitions =
            new Dictionary<int, ErrorDefinition>
            {
                [400] = new(
                    400,
                    "Bad Request",
                    "The request could not be processed.",
                    "Please review the information provided and try again.",
                    true,
                    true),

                //[401] = new(
                //    401,
                //    "Unauthorized",
                //    "You must sign in before accessing this page.",
                //    "Please authenticate and try again.",
                //    false,
                //    true),

                //[403] = new(
                //    403,
                //    "Access Denied",
                //    "You don't have permission to access this resource.",
                //    "If you believe this is incorrect, contact your administrator.",
                //    true,
                //    true),

                [404] = new(
                    404,
                    "Page Not Found",
                    "The page you're looking for could not be found.",
                    "It may have been moved, renamed, or is no longer available.",
                    true,
                    true),

                //[409] = new(
                //    409,
                //    "Conflict Detected",
                //    "The requested operation could not be completed.",
                //    "The resource has changed since it was loaded. Please refresh and try again.",
                //    true,
                //    true),

                [500] = new(
                    500,
                    "Something Unexpected Happened",
                    "We couldn't complete your request right now.",
                    "Your work is safe and no changes have been lost. Please try again in a few moments.",
                    true,
                    true),

                [503] = new(
                    503,
                    "Service Temporarily Unavailable",
                    "The service is temporarily unavailable.",
                    "Please try again later.",
                    true,
                    true),

                [0] = new(
                    0,
                    "Unexpected Error",
                    "An unexpected error has occurred.",
                    "Please try again later or contact your administrator.",
                    true,
                    true)
            };

        /// <inheritdoc/>
        public Task<ErrorPageDTO> GetErrorPageAsync(
            int statusCode,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!ErrorDefinitions.TryGetValue(statusCode, out ErrorDefinition? definition))
            {
                definition = ErrorDefinitions[0];
            }

            ErrorPageDTO response = new()
            {
                StatusCode = definition.StatusCode,
                Title = definition.Title,
                Message = definition.Message,
                Description = definition.Description,
                ShowGoBack = definition.ShowGoBack,
                ShowDashboardButton = definition.ShowDashboardButton,
                ErrorReference = GenerateReference(),
                DashboardUrl = "/",
                SupportContact = "EPPMS Administrator"
            };

            return Task.FromResult(response);
        }

        private static string GenerateReference()
        {
            return Guid.NewGuid()
                .ToString("N")[..12]
                .ToUpperInvariant();
        }
    }
}