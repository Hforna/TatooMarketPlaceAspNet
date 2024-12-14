using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Responses.Tattoo;

namespace TatooMarket.Communication.Responses.Studio
{
    public class ResponseStudioPriceCatalogShort
    {
        public string CurrencyType { get; set; }
        public List<ResponseTattooPlacePrice> PlaceCatalog { get; set; }
        public List<ResponseTattooStylePrice> StyleCatalog { get; set; }
    }
}
