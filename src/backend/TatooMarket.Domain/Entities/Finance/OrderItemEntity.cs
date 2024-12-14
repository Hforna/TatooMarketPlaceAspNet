using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Tattoo;
using TatooMarket.Domain.Enum;

namespace TatooMarket.Domain.Entities.Finance
{
    [Table("orderItems")]
    public class OrderItemEntity : BaseEntity
    {
        public long StudioId { get; set; }
        public Studio Studio { get; set; }
        public long OrderId { get; set; }
        public float Price { get; set; }
        public long TattooStyleId { get; set; }
        public long BodyPlacementId { get; set; }
        public TattooStyleEnum TattooStyle { get; set; }
        public BodyPlacementEnum BodyPlacement { get; set; }

        public override string ToString()
        {
            return $"{TattooStyle.ToString()} on {BodyPlacement.ToString()}";
        }
    }
}
