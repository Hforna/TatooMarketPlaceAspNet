using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Repositories.Security.Cryptography;

namespace TatooMarket.Infrastructure.Security.Cryptography
{
    public class BcryptNet : IPasswordCryptography
    {
        public string Cryptography(string key)
        {
            return BCrypt.Net.BCrypt.HashPassword(key);
        }

        public bool IsValid(string key, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(key, hashPassword);
        }
    }
}
