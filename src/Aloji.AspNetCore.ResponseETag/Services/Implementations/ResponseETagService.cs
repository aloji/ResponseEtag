using Aloji.AspNetCore.ResponseETag.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Aloji.AspNetCore.ResponseETag.Services.Implementations
{
    public class ResponseETagService : IResponseETagService
    {
        public bool ShouldGenerateHeaderResponse(HttpContext context)
        {
            var result = context.Response.StatusCode == StatusCodes.Status200OK
                && !context.Response.Headers.ContainsKey(HeaderNames.ETag)
                && context.Request.Method == HttpMethods.Get;

            return result;
        }

        public bool IfNoneMatchIsValid(HttpContext context)
        {
            var result = false;
            if (context.Request.Headers.Keys.Contains(HeaderNames.IfNoneMatch)
                && context.Response.Headers.ContainsKey(HeaderNames.ETag))
            {
                var eTagValueRequest = context.Request.Headers[HeaderNames.IfNoneMatch].ToString();
                var eTagValueResponse = context.Response.Headers[HeaderNames.ETag].ToString();

                result = eTagValueRequest
                    .Equals(eTagValueResponse, StringComparison.InvariantCultureIgnoreCase);
            }
            return result;
        }

        public string GenerateETag(string body)
        {
            var bodyAsBytes = Encoding.UTF8.GetBytes(body);
            var result = string.Empty;
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(bodyAsBytes);
                var value = BitConverter.ToString(hash)
                    .Replace("-", "");
                
                result = $"\"{value}\""; //https://stackoverflow.com/questions/6719214/syntax-for-etag
            }
            return result;
        }
    }
}
