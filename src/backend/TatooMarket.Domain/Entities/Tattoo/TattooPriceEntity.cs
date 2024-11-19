using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Enum;

namespace TatooMarket.Domain.Entities.Tattoo
{
    [Table("tattoosPrice")]
    public class TattooPriceEntity : BaseEntity
    {
        public TattooSizeEnum TattooSize { get; set; }
        public BodyPlacementEnum BodyPlacement { get; set; }
        public float Price { get; set; }
        [ForeignKey("Studio")]
        public long StudioId { get; set; }
        public Studio? Studio { get; set; }
    }
}
