using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Domain.Repositories.StudioRepository
{
    public interface IStudioReadOnly
    {
        public Task<bool> StudioNameExists(string name);
    }
}
