using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Domain.Entities.Finance
{
    [Table("balances")]
    public class BalanceEntity : BaseEntity
    {
        public long StudioId { get; set; }
        public float Balance { get; set; } = 0;
        public float AmountDrawOut { get; set; } = 0;
        public DateTime? LastDrawOut { get; set; }
    }
}
