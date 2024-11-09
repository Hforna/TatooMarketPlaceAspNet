using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Tattoo;

namespace TatooMarket.Domain.Entities.Identity
{
    public class UserEntity : IdentityUser<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
        public DateTime LastAccess { get; set; } = DateTime.UtcNow;
        [MinLength(8, ErrorMessage = "Password length must be greater than 7")]
        [Required]
        public required string Password { get; set; }
        public long? StudioId { get; set; }
        [ForeignKey("StudioId")]
        public Studio? Studio { get; set; }
        public bool IsSeller { get; set; } = false;
        public string? UserImage { get; set; }
        public Guid UserIdentifier { get; set; }
    }
}
