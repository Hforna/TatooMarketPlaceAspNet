using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Tattoo;
using TatooMarket.Domain.Repositories.Tattoo;

namespace TatooMarket.Infrastructure.DataEntity
{
    public class TattooDbContext : ITattooReadOnly
    {
        private readonly ProjectDbContext _dbContext;

        public TattooDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public async Task<IList<TattooEntity>> GetRecentTattoss(Studio studio)
        {
            return await _dbContext.Tattos.Where(d => d.Studio == studio).OrderBy(d => d.CreatedOn).Take(5).ToListAsync();
        }
    }
}
