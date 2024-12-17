using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Finance;
using TatooMarket.Domain.Repositories.Finance;

namespace TatooMarket.Infrastructure.DataEntity
{
    public class FinanceDbContext : IFinanceReadOnly, IFinanceWriteOnly
    {
        private readonly ProjectDbContext _dbContext;

        public FinanceDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public async Task AddBalance(BalanceEntity balance)
        {
            await _dbContext.balances.AddAsync(balance);
        }

        public async Task AddBankAccount(StudioBankAccountEntity bankAccount)
        {
            await _dbContext.studioBankAccounts.AddAsync(bankAccount);
        }

        public async Task<BalanceEntity?> BalanceByStudio(long studioId)
        {
            return await _dbContext.balances.SingleOrDefaultAsync(d => d.StudioId == studioId);
        }

        public void UpdateBalance(BalanceEntity balance)
        {
            _dbContext.balances.Update(balance);
        }
    }
}
