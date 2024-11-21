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

        public async Task AddTattooPrice(TattooPriceEntity tattooPrice)
        {
            await _dbContext.tattoosPrice.AddAsync(tattooPrice);
        }

        public void Delete(TattooEntity tattoo)
        {
            _dbContext.Tattos.Remove(tattoo);
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

        public async Task<bool> StudioIsOwnTattoo(Studio studio, long Id)
        {
            return await _dbContext.Tattos.AnyAsync(d => d.Id == Id && d.StudioId == studio.Id);
        }

        public async Task<TattooEntity?> TattooById(long id)
        {
            return await _dbContext.Tattos.SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<TattooPriceEntity>?> TattooByStudio(Studio studio)
        {
            return await _dbContext.tattoosPrice.Where(d => d.StudioId == studio.Id).ToListAsync();
        }

        public async Task<TattooPriceEntity?> TattooPriceById(long id)
        {
            return await _dbContext.tattoosPrice.FirstOrDefaultAsync(d => d.Id == id);
        }

        public void Update(TattooEntity tattoo)
        {
            _dbContext.Tattos.Update(tattoo);
        }

        public void UpdateTattooPrice(TattooPriceEntity tattooPrice)
        {
            _dbContext.tattoosPrice.Update(tattooPrice);
        }

        public async Task<IList<TattooEntity>> WeeksTattoos(DateTime date)
        {
            return await _dbContext.Tattos
                .Where(d => d.CreatedOn.AddDays(7) >= date)
                .OrderByDescending(d => d.Note)
                .Take(10)
                .ToListAsync();
        }
    }
}
