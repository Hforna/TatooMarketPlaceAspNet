using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Communication.Requests.Tattoo
{
    public class RequestCreateTattooReview
    {
        public string TattooId { get; set; }
        public string Comment { get; set; }
        public int Note {  get; set; }
    }
}
