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

        public async Task<bool> StudioNameExists(string name)
        {
            return await _dbContext.Studios.AnyAsync(d => d.StudioName == name);
        }
    }
}
