using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Domain.Repositories.Security.Token
{
    public interface IGetHeaderToken
    {
        public string? GetToken();
    }
}
