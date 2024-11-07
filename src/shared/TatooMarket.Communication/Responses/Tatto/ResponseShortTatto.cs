using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Communication.Responses.Tatto
{
    public class ResponseShortTatto
    {
        public long StudioId { get; set; }
        public string? TattoImage { get; set; }
        public int Style { get; set; }
        public float Price { get; set; }
    }
}
