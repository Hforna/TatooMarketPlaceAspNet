using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Finance;
using TatooMarket.Domain.Entities.Identity;

namespace TatooMarket.Domain.Repositories.Orders
{
    public interface IOrderReadOnly
    {
        public Task<Order?> OrderByUser(UserEntity user);
    }
}
