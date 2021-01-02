using Microsoft.AspNetCore.Builder;

namespace MrLocalApi.Controllers.Exceptions
{
    public static class AuthenticationMiddlewareExtensions
    {
        public static void ConfigureCustomAuthenticationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
