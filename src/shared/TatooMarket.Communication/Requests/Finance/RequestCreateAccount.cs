using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Communication.Requests.Finance
{
    public class RequestCreateAccount
    {
        public string PhoneNumber { get; set; }
        public string OwnerName { get; set; }
        public string Email { get; set; }
        public string BranchCode { get; set; }
        public string CurrencyType { get; set; }
    }
}
