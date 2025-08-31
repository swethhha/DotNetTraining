using Microsoft.AspNetCore.Builder;
using ShopTrackPro.API.Middleware;

namespace ShopTrackPro.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
