using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Entities.Tattoo;

namespace TatooMarket.Domain.Repositories.StudioRepository
{
    public interface IStudioReadOnly
    {
        public Task<bool> StudioNameExists(string name);

        public Task<Studio?> StudioByOwner(UserEntity user);
    }
}
