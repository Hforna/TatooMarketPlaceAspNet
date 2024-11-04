using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Domain.Repositories.Security.Cryptography
{
    public interface IPasswordCryptography
    {
        public string Cryptography(string key);
        public bool IsValid(string key, string hashPassword);
    }
}
