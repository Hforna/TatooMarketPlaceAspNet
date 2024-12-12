using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Identity;

namespace TatooMarket.Domain.Entities.Finance
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public UserEntity? User { get; set; }
        public float TotalPrice { get; set; }
        public IList<OrderItemEntity> OrderItems { get; set; } = [];
    }
}
