using Microsoft.AspNetCore.Mvc;
using TatooMarket.Api.Filters;

namespace TatooMarket.Api.Attributes
{
    public class AuthorizationUserAttribute : TypeFilterAttribute
    {
        public AuthorizationUserAttribute() : base(typeof(UserAuthorization))
        {
        }
    }
}
