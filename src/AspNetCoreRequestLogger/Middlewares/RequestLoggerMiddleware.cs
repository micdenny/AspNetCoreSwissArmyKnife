using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AspNetCoreRequestLogger.Middlewares
{
    /// <summary>
    /// WARNING: DO NOT USE ON SERVER THAT SUPPORTS UPLOADS OF BIG RESOURCES.
    /// </summary>
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggerMiddleware> _logger;

        public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // avoid to read the request if not necessary, could be quite expensive
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    var rawRequest = context.Request.ToRaw();
                    _logger.LogDebug("Request: {0}", rawRequest);
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(0, ex, "Failed to log an Http Request");
            }

            await _next(context);
        }
    }
}
