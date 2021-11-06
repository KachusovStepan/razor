using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BadNews.Elevation
{
    public class ElevationMiddleware
    {
        private readonly RequestDelegate next;
    
        public ElevationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
    
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            if (path == "/elevation")
            {
                var query = context.Request.Query;
                if (query.ContainsKey("up"))
                {
                    context.Response.Cookies.Append(ElevationConstants.CookieName, ElevationConstants.CookieValue,
                    new CookieOptions
                    {
                        HttpOnly = true
                    });
                }
                else
                {
                    context.Response.Cookies.Delete(ElevationConstants.CookieName);
                }
                context.Response.Redirect("/");
                return;
            }

            await next(context);
        }
    }
}
