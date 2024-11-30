using TatooMarket.Domain.Repositories.Sessions;

namespace TatooMarket.Api.Filters
{
    public class GetCustomerSession : IGetCustomerSession
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public GetCustomerSession(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;

        public string GetSessionIdentifier(string typeSession)
        {
            var context = _contextAccessor.HttpContext;

            if(context.Session.TryGetValue(typeSession, out var type) == false)
            {
                var identifier = Guid.NewGuid().ToString();

                context.Session.SetString(typeSession, identifier);
            }

            return context.Session.GetString(typeSession);
        }

        public bool ThereIsSession(string typeSession)
        {
            return _contextAccessor.HttpContext.Session.TryGetValue(typeSession, out var type);
        }
    }
}
