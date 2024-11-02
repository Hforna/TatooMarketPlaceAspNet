using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Domain.Entities.Identity
{
    public class RoleEntity : IdentityRole<long>
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
