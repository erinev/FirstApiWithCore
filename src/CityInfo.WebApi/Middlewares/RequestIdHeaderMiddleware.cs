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
    public static class RequestIdHeaderMiddlewareExtensions
    {
        /// <summary>
        ///  Extension method which exposes the middleware through IApplicationBuilder
        /// </summary>
        /// <param name="builder">IApplicationBuilder</param>
        /// <returns>Extended IApplicationBuilder</returns>
        public static IApplicationBuilder UseRequestIdHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIdHeaderMiddleware>();
        }
    }

    /// <summary>
    /// Preserves RequestId header to context and adds it to response
    /// </summary>
    public class RequestIdHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _requestIdHeaderName = "RequestId";

        /// <summary>
        /// Injects request delegate
        /// </summary>
        /// <param name="next">Request delegate</param>
        public RequestIdHeaderMiddleware(RequestDelegate next)
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
            if (context.Request.Headers.ContainsKey(_requestIdHeaderName))
            {
                StringValues headerValues;

                if (context.Request.Headers.TryGetValue(_requestIdHeaderName, out headerValues))
                {
                    RequestId.Current = headerValues.First();
                }
            }
            else
            {
                RequestId.Current = Guid.NewGuid().ToString();
            }

            context.Response.Headers.Add(_requestIdHeaderName, new StringValues(RequestId.Current));

            return _next(context);
        }
    }
}