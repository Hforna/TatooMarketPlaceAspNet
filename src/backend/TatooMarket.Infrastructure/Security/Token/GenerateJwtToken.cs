using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Repositories.Security.Token;

namespace TatooMarket.Infrastructure.Security.Token
{
    public class GenerateJwtToken : ITokenGenerator
    {
        private readonly string _signKey;

        public GenerateJwtToken(string signKey) => _signKey = signKey;
        public string GenerateToken(Guid uid)
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.Sid, uid.ToString()) };
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(40),
                SigningCredentials = new SigningCredentials(GetSignKeyAsSecurity.GenerateSecurityKey(_signKey), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateJwtSecurityToken(descriptor);

            return token.ToString();
        }
    }
}
