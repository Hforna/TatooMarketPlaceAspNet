using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Finance;

namespace TatooMarket.Domain.Repositories.Finance
{
    public interface IFinanceWriteOnly
    {
        public Task AddBankAccount(StudioBankAccountEntity bankAccount);
        public Task AddBalance(BalanceEntity balance);
        public void UpdateBalance(BalanceEntity balance);
    }
}
