using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Communication.Requests.Order
{
    public class RequestAddTattooToOrder
    {
        public string TattooStyleId { get; set; }
        public string TattooPlaceId { get; set; }
    }
}
