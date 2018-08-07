using Microsoft.AspNetCore.Http;

namespace Aloji.AspNetCore.ResponseETag.Services.Contracts
{
    public interface IResponseETagService
    {
        string GenerateETag(string body);
        bool ShouldGenerateHeaderResponse(HttpContext context);
        bool IfNoneMatchIsValid(HttpContext context);
    }
}
