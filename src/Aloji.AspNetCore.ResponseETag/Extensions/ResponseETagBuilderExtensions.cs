using Aloji.AspNetCore.ResponseETag.Middleware;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class ResponseETagBuilderExtensions
    {
        public static IApplicationBuilder UseResponseETag(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.UseMiddleware<ResponseETagMiddleware>();
            builder.UseMiddleware<ResponseStatus304Middleware>();
            return builder;
        }
    }
}