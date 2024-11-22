using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities;
using TatooMarket.Domain.Repositories.Address;

namespace TatooMarket.Infrastructure.DataEntity
{
    public class AddressDbContext : IAddressReadOnly, IAddressWriteOnly
    {
        private readonly ProjectDbContext _dbContext;

        public AddressDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(StudioAddress studioAddress)
        {
            await _dbContext.studioAddresses.AddAsync(studioAddress);
        }
    }
}
