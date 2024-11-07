using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Communication.Requests.Login
{
    public class RequestLogin
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
