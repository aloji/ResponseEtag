using Aloji.AspNetCore.ResponseETag.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Aloji.AspNetCore.ResponseETag.Middleware
{
    public class ResponseETagMiddleware
    {
        readonly RequestDelegate next;
        readonly IResponseETagService iResponseETagService;

        public ResponseETagMiddleware(RequestDelegate next, IResponseETagService iResponseETagService)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.iResponseETagService = iResponseETagService ?? throw new ArgumentNullException(nameof(iResponseETagService));
        }

        public async Task Invoke(HttpContext context)
        {
            var bodyStream = context.Response.Body;
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    context.Response.Body = memoryStream;

                    await next(context);

                    if (this.iResponseETagService.ShouldGenerateHeaderResponse(context))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        var responseBodyContent = new StreamReader(memoryStream).ReadToEnd();
                        var eTagValue = this.iResponseETagService.GenerateETag(responseBodyContent);
                        context.Response.Headers.Add(HeaderNames.ETag, eTagValue);
                    }

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await memoryStream.CopyToAsync(bodyStream);
                }
            }
            finally
            {
                context.Response.Body = bodyStream;
            }
        }
    }
}
