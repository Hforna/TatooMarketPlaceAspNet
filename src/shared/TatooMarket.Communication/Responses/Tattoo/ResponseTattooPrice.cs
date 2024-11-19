using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Enums;

namespace TatooMarket.Communication.Responses.Tattoo
{
    public class ResponseTattooPrice
    {
        public string? Id { get; set; }
        public string? StudioId { get; set; }
        public float Price { get; set; }
        public TattooSizeEnum TattooSize { get; set; }
        public BodyPlacementEnum BodyPlacement { get; set; }
    }
}
