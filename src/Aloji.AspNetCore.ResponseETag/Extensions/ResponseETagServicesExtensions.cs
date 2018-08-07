using Aloji.AspNetCore.ResponseETag.Services.Contracts;
using Aloji.AspNetCore.ResponseETag.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class ResponseETagServicesExtensions
    {
        public static IServiceCollection AddResponseETag(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IResponseETagService, ResponseETagService>();
            return services;
        }
    }
}
