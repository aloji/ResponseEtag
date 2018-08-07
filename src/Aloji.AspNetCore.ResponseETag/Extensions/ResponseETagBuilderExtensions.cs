using Aloji.AspNetCore.ResponseETag.Middleware;

namespace Microsoft.AspNetCore.Builder
{
    public static class ResponseETagBuilderExtensions
    {
        public static IApplicationBuilder UseResponseETag(this IApplicationBuilder builder)
        {
            var result = builder;
            builder.UseMiddleware<ResponseETagMiddleware>();
            builder.UseMiddleware<ResponseStatus304Middleware>();
            return result;
        }
    }
}
