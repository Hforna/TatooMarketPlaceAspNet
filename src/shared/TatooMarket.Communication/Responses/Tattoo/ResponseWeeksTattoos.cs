using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Responses.Tatto;

namespace TatooMarket.Communication.Responses.Tattoo
{
    public class ResponseWeeksTattoos
    {
        public IList<ResponseShortTatto>? Tattoos { get; set; }
    }
}
