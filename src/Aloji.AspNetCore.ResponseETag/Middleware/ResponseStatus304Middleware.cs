using Aloji.AspNetCore.ResponseETag.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Aloji.AspNetCore.ResponseETag.Middleware
{
    public class ResponseStatus304Middleware
    {
        readonly RequestDelegate next;
        readonly IResponseETagService iResponseETagService;

        public ResponseStatus304Middleware(RequestDelegate next, IResponseETagService iResponseETagService)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.iResponseETagService = iResponseETagService ?? throw new ArgumentNullException(nameof(iResponseETagService));
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() => {
                if (this.iResponseETagService.IfNoneMatchIsValid(context))
                {
                    context.Response.Clear();
                    context.Response.StatusCode = StatusCodes.Status304NotModified;
                }
                return Task.FromResult(0);
            });

            await next(context);
        }
    }
}
