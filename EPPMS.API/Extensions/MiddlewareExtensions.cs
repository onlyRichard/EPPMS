using EPPMS.API.Middleware;

namespace EPPMS.API.Extensions
{
    public static class MiddlewareExtensions
    {
        #region Public Methods
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
        #endregion
    }
}