using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Enums;

namespace TatooMarket.Communication.Responses.Finance
{
    public class ResponseCreateFinanceAccount
    {
        public string AccountId { get; set; }
        public string BalanceId { get; set; }
        public CurrencyEnum CurrencyType { get; set; }
    }
}
