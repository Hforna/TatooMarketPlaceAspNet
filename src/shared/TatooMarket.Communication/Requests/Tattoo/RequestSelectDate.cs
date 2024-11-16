using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Communication.Requests.Tattoo
{
    public class RequestSelectDate
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
