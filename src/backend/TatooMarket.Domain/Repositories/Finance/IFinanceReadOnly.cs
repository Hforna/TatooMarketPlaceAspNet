using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Finance;

namespace TatooMarket.Domain.Repositories.Finance
{
    public interface IFinanceReadOnly
    {
        public Task<BalanceEntity?> BalanceByStudio(long studioId);
    }
}
