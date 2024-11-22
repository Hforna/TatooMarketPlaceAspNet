using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities;
using TatooMarket.Domain.Entities.Identity;

namespace TatooMarket.Domain.Entities.Tattoo
{
    [Table("studios")]
    public class Studio : BaseEntity
    {
        public required string StudioName { get; set; }
        public long OwnerId { get; set; }
        [Required]
        [ForeignKey("OwnerId")]
        public UserEntity Owner { get; set; }
        public int CustomerQuantity { get; set; } = 0;
        [Range(1, 5, ErrorMessage = "Range must be between 1 and 5")]
        public int Note { get; set; } = 0;
        [NotMapped]
        public ICollection<UserEntity>? Customers { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageStudio { get; set; }
        [InverseProperty("Studio")]
        public ICollection<TattooEntity>? StudioTattoss { get; set; }
    }
}
