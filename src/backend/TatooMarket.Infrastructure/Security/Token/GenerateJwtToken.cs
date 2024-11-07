using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TatooMarket.Domain.Repositories.Security.Token;

namespace TatooMarket.Infrastructure.Security.Token
{
    public class GenerateJwtToken : ITokenGenerator
    {
        private readonly string _signKey;

        public GenerateJwtToken(string signKey) => _signKey = signKey;

        public string GenerateToken(Guid uid, List<Claim>? claimsUser = null!)
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.Sid, uid.ToString()) };

            if (claimsUser is not null)
                claims.AddRange(claimsUser);

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(40),
                SigningCredentials = new SigningCredentials(GetSignKeyAsSecurity.GenerateSecurityKey(_signKey), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}