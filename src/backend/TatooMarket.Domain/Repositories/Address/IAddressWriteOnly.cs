using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities;

namespace TatooMarket.Domain.Repositories.Address
{
    public interface IAddressWriteOnly
    {
        public Task Add(StudioAddress studioAddress);
    }
}
