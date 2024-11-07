using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Tattoo;

namespace TatooMarket.Domain.Repositories.Tattoo
{
    public interface ITattooReadOnly
    {
        public Task<IList<TattooEntity>> GetRecentTattoss(Studio studio);
    }
}
