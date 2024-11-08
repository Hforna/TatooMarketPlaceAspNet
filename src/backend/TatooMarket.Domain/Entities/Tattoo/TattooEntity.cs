using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities;
using TatooMarket.Domain.Enum;

namespace TatooMarket.Domain.Entities.Tattoo
{
    [Table("tattoos")]
    public class TattooEntity : BaseEntity
    {
        [ForeignKey("Studio")]
        public long StudioId { get; set; }
        public Studio? Studio { get; set; }
        public string? TattooImage { get; set; }
        public BodyPlacementEnum BodyPlacement { get; set; }
        public TattooSizeEnum Size { get; set; }
        public TattooStyleEnum Style { get; set; }
        public float Price { get; set; }
    }
}
