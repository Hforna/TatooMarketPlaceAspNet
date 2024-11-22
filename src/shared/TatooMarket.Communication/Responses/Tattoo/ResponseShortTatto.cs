using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Communication.Responses.Tatto
{
    public class ResponseShortTatto
    {
        public string Id { get; set; }
        public string StudioId { get; set; }
        public string? TattoImage { get; set; }
        public string? CustomerId { get; set; }
        public int Style { get; set; }
        public float Price { get; set; }
    }
}
