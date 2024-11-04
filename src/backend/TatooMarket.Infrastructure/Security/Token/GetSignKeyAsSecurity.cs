using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Infrastructure.Security.Token
{
    public static class GetSignKeyAsSecurity
    {
        public static SecurityKey GenerateSecurityKey(string signKey)
        {
            var securityKey = Encoding.UTF8.GetBytes(signKey);

            return new SymmetricSecurityKey(securityKey);
        }
    }
}
