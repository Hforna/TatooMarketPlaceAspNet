using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Tattoo;

namespace TatooMarket.Domain.Repositories.StudioRepository
{
    public interface IStudioWriteOnly
    {
        public void Add(Studio studio);
        public void Update(Studio studio);
    }
}
