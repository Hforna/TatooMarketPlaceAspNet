using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Enums;

namespace TatooMarket.Domain.Entities.Finance
{
    [Table("studioBankAccounts")]
    public class StudioBankAccountEntity : BaseEntity
    {
        public long StudioId { get; set; }
        public string PhoneNumber { get; set; }
        public string OwnerName { get; set; }
        public string Email {  get; set; }
        public string BranchCode { get; set; }
        public CurrencyEnum CurrencyType { get; set; }
    }
}
