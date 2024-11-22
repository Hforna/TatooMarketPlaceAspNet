using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Communication.Requests.Address
{
    public class RequestCreateAddress
    {
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
