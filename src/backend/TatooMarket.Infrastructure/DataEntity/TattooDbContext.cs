using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Communication;
using TatooMarket.Domain.Entities.Tattoo;
using TatooMarket.Domain.Repositories.Tattoo;
using X.PagedList;

namespace TatooMarket.Infrastructure.DataEntity
{
    public class TattooDbContext : ITattooReadOnly, ITattooWriteOnly
    {
        private readonly ProjectDbContext _dbContext;

        public TattooDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(TattooEntity tattoo)
        {
            await _dbContext.Tattos.AddAsync(tattoo);
        }

        public async Task<IList<TattooEntity>> GetRecentTattoss(Studio studio)
        {
            return await _dbContext.Tattos.Where(d => d.Studio == studio).OrderBy(d => d.CreatedOn).Take(5).ToListAsync();
        }

        public async Task<IPagedList<TattooEntity>> GetStudioTattoos(Studio studio, int pageNumber)
        {
            return await _dbContext.Tattos.Where(d => d.StudioId == studio.Id).ToPagedListAsync(pageNumber, 6);
        }

        public async Task<IList<ReviewEntity>> GetTattooReviews(TattooEntity tattoo)
        {
            return await _dbContext.Reviews.Where(d => d.Tattoo == tattoo).ToListAsync();
        }

        public async Task<TattooEntity?> TattooById(long id)
        {
            return await _dbContext.Tattos.SingleOrDefaultAsync(d => d.Id == id);
        }

        public void Update(TattooEntity tattoo)
        {
            _dbContext.Tattos.Update(tattoo);
        }
    }
}
