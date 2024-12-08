using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Enums;

namespace TatooMarket.Communication.Requests.Tattoo
{
    public class RequestCreateTattooPlacePrice
    {
        public BodyPlacementEnum BodyPlacement { get; set; }
        public float Price { get; set; }
        public CurrencyEnum CurrencyType { get; set; }
    }
}
