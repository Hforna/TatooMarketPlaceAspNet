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
    [Table("tattooStylePrice")]
    public class TattooStylePriceEntity : BaseEntity
    {
        public long StudioId { get; set; }
        public TattooStyleEnum TattooStyle { get; set; }
        public CurrencyEnum CurrencyType { get; set; }
        public int Price { get; set; }
        [ForeignKey("StudioId")]
        public Studio Studio { get; set; }
    }
}
