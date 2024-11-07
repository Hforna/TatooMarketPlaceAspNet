using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.User;

namespace TatooMarket.Infrastructure.Security.Token
{
    public class GetUserByToken : IGetUserByToken
    {
        private readonly IGetHeaderToken _getHeader;
        private readonly IUserReadRepository _userRead;

        public GetUserByToken(IGetHeaderToken getHeader, IUserReadRepository userRead)
        {
            _getHeader = getHeader;
            _userRead = userRead;
        }

        public async Task<UserEntity?> GetUser()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var readToken = tokenHandler.ReadJwtToken(_getHeader.GetToken());

            var userIdentfier = Guid.Parse(readToken.Claims.First(d => d.Type == ClaimTypes.Sid).Value);

            var user = await _userRead.UserByUid(userIdentfier);

            return user;
        }
    }
}