using AspNetCoreRequestLogger.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class RequestLogMiddlewareExtensions
    {
        /// <summary>
        /// WARNING: DO NOT USE ON SERVER THAT SUPPORTS UPLOADS OF BIG RESOURCES.
        /// </summary>
        public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggerMiddleware>();
        }
    }
}
