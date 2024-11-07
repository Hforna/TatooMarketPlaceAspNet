using Microsoft.AspNetCore.Mvc.Filters;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Api.Filters
{
    public class UserAuthorization : IAsyncAuthorizationFilter
    {
        private readonly IGetHeaderToken _getHeader;
        private readonly IGetUserByToken _getUserByToken;
        private readonly ITokenValidator _tokenValidator;

        public UserAuthorization(IGetHeaderToken getHeader, IGetUserByToken getUserByToken, ITokenValidator tokenValidator)
        {
            _getHeader = getHeader;
            _getUserByToken = getUserByToken;
            _tokenValidator = tokenValidator;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var token = _getHeader.GetToken();

            var user = await _getUserByToken.GetUser();

            if (user is null)
                throw new UserException(ResourceExceptMessages.USER_DOESNT_EXISTS);

            var validate = _tokenValidator.Validate(token);

            if (validate == false)
                throw new UserException(ResourceExceptMessages.INVALID_TOKEN);
        }
    }
}
