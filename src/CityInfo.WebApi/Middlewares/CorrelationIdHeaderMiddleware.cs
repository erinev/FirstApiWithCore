using System;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Configuration.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace CityInfo.WebApi.Middlewares
{
    /// <summary>
    /// Exposes the middleware through IApplicationBuilder
    /// </summary>
    public static class CorrelationIdHeaderMiddlewareExtensions
    {
        /// <summary>
        ///  Extension method which exposes the middleware through IApplicationBuilder
        /// </summary>
        /// <param name="builder">IApplicationBuilder</param>
        /// <returns>Extended IApplicationBuilder</returns>
        public static IApplicationBuilder UseCorrelationIdHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationIdHeaderMiddleware>();
        }
    }

    /// <summary>
    /// Preserves CorrelationId header to context and adds it to response
    /// </summary>
    public class CorrelationIdHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _correlationIdHeaderName = "CorrelationId";

        /// <summary>
        /// Injects request delegate
        /// </summary>
        /// <param name="next">Request delegate</param>
        public CorrelationIdHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes middleware
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns>Task</returns>
        public Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(_correlationIdHeaderName))
            {
                StringValues headerValues;

                if (context.Request.Headers.TryGetValue(_correlationIdHeaderName, out headerValues))
                {
                    CorrelationId.Current = headerValues.First();
                }
            }
            else
            {
                CorrelationId.Current = Guid.NewGuid().ToString();
            }

            context.Response.Headers.Add(_correlationIdHeaderName, new StringValues(CorrelationId.Current));


            return _next(context);
        }
    }
}