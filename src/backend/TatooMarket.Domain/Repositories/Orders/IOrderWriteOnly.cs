using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Finance;

namespace TatooMarket.Domain.Repositories.Orders
{
    public interface IOrderWriteOnly
    {
        public Task AddOrder(Order order);
        public void UpdateOrder(Order order);
        public Task AddOrderItem(OrderItemEntity orderItem);
    }
}
