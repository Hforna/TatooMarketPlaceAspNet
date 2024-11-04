using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Repositories;

namespace TatooMarket.Infrastructure.DataEntity
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _dbContext;

        public UnitOfWork(ProjectDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
