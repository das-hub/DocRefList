using DocRefList.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace DocRefList.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
