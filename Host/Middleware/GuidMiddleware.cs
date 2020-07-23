using System;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Domain.Middleware
{
    /// <summary>
    /// </summary>
    public class GuidMiddleware
    {
        private readonly RequestDelegate _next;

        public GuidMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IGuidService service)
        {
            httpContext.Items["guid"] = service.GetGuid();
            await _next(httpContext);
        }
    }
}