using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Entities.Tattoo;

namespace TatooMarket.Domain.Entities.Communication
{
    [Table("reviews")]
    public class ReviewEntity : BaseEntity
    {
        [ForeignKey("Studio")]
        public long StudioId { get; set; }
        public Studio Studio { get; set; }
        [ForeignKey("TattooEntity")]
        public long TattooId { get; set; }
        [ForeignKey("TattooId")]
        public TattooEntity Tattoo { get; set; }
        [ForeignKey("Customer")]
        public long? CustomerId { get; set; }
        public UserEntity Customer { get; set; }
        public int Note {  get; set; }
        [MaxLength(500, ErrorMessage = "Comment is very long")]
        public string Comment { get; set; }
    }
}
