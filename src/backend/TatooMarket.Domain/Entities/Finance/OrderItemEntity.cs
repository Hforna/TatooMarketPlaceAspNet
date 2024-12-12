using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Enum;

namespace TatooMarket.Domain.Entities.Finance
{
    [Table("orderItems")]
    public class OrderItemEntity : BaseEntity
    {
        public long StudioId { get; set; }
        public long OrderId { get; set; }
        public float Price { get; set; }
        public long TattooStyle { get; set; }
        public long BodyPlacement { get; set; }

        public override string ToString()
        {
            return $"{TattooStyle} on {BodyPlacement}";
        }
    }
}
