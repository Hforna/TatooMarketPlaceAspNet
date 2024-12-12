using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Finance;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Repositories.Orders;

namespace TatooMarket.Infrastructure.DataEntity
{
    public class OrderDbContext : IOrderReadOnly, IOrderWriteOnly
    {
        private readonly ProjectDbContext _dbContext;

        public OrderDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public async Task AddOrder(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public async Task<Order?> OrderByUser(UserEntity user)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(o => o.User == user && o.Active);
        }

        public void UpdateOrder(Order order)
        {
            _dbContext.Orders.Update(order);
        }

        public async Task AddOrderItem(OrderItemEntity orderItem)
        {
            await _dbContext.orderItems.AddAsync(orderItem);
        }
    }
}
