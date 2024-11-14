using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Entities.Tattoo;
using TatooMarket.Domain.Repositories.StudioRepository;
using X.PagedList;

namespace TatooMarket.Infrastructure.DataEntity
{
    public class StudioDbContext : IStudioReadOnly, IStudioWriteOnly
    {
        private readonly ProjectDbContext _dbContext;

        public StudioDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public void Add(Studio studio)
        {
            _dbContext.Studios.Add(studio);
        }

        public async Task<Studio?> StudioByOwner(UserEntity user)
        {
            return await _dbContext.Studios.FirstOrDefaultAsync(s => s.OwnerId == user.Id);
        }

        public  X.PagedList.IPagedList<Studio?> GetStudios(int page)
        {
            var studios = _dbContext.Set<Studio>().Where(d => d.Active);

            if (studios is null)
                return null!;

            return studios.ToPagedList(page, 4);
        }

        public async Task<bool> StudioNameExists(string name)
        {
            return await _dbContext.Studios.AnyAsync(d => d.StudioName == name);
        }

        public async Task<Studio?> StudioById(long id)
        {
            return await _dbContext.Studios.SingleOrDefaultAsync(d => d.Id == id);
        }
    }
}
