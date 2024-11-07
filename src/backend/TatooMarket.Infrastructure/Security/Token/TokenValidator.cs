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
    public class TokenValidator : ITokenValidator
    {
        private readonly string _signKey;

        public TokenValidator(string signKey)
        {
            _signKey = signKey;
        }

        public bool Validate(string token)
        {
            var validator = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = GetSignKeyAsSecurity.GenerateSecurityKey(token),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var read = tokenHandler.ValidateToken(token, validator, out _);

            var uid = read.Claims.FirstOrDefault(d => d.Type == ClaimTypes.Sid)!.Value;

            if (uid is null)
                return false;
            return true;
        }
    }
}
