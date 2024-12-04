using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Enum;
using TatooMarket.Domain.Enums;

namespace TatooMarket.Domain.Entities.Tattoo
{
    [Table("tattooPlacePrice")]
    public class TattooPlacePriceEntity : BaseEntity
    {
        public BodyPlacementEnum BodyPlacement { get; set; }
        public CurrencyEnum CurrencyType { get; set; }
        public float Price { get; set; }
        [ForeignKey("Studio")]
        public long StudioId { get; set; }
        public Studio? Studio { get; set; }
    }
}
