using TatooMarket.Domain.Repositories.Security.Token;

namespace TatooMarket.Api.Filters
{
    public class GetHeaderToken : IGetHeaderToken
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public GetHeaderToken(IHttpContextAccessor httpContext) => HttpContextAccessor = httpContext;

        public string? GetToken()
        {
            var token = HttpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(token))
                return null;

            var bearerIndex = "Bearer ".Length;

            return token[bearerIndex..].Trim();
        }
    }
}
