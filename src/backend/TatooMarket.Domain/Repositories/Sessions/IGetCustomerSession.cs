using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Domain.Repositories.Sessions
{
    public interface IGetCustomerSession
    {
        public string GetSessionIdentifier(string typeSession);
        public bool ThereIsSession(string typeSession);
    }
}
