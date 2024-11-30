using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Enums;

namespace TatooMarket.Communication.Requests.Finance
{
    public class RequestCreateFinanceAccount
    {
        public string PhoneNumber { get; set; }
        public string OwnerName { get; set; }
        public string Email { get; set; }
        public string BranchCode { get; set; }
        public CurrencyEnum CurrencyType { get; set; }
    }
}
