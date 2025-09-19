using AP.MyGameStore.WebAPI.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Demo.WebAPI.Extensions
{
    public static class Registrator
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware (this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
