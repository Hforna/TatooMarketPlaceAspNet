using Microsoft.IdentityModel.Tokens;
using System.Text;

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