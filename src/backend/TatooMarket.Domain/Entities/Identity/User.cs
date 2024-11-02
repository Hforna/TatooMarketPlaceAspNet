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
    public class User : IdentityUser<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastAccess { get; set; } = DateTime.UtcNow;
        [MinLength(8, ErrorMessage = "Password length must be greater than 7")]
        [Required]
        public required string Password { get; set; }
        public bool IsSeller { get; set; } = false;
        [InverseProperty("Owner")]
        [NotMapped]
        public Studio? Studio { get; set; }
        public string? UserImage { get; set; }
    }
}
