using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Enums;

namespace TatooMarket.Communication.Requests.Tattoo
{
    public class RequestCreateTattoo
    {
        public IFormFile TattooImage { get; set; }
        public BodyPlacementEnum BodyPlacement { get; set; }
        public TattooSizeEnum Size { get; set; }
        public TattooStyleEnum Style { get; set; }
        public float Price { get; set; }
        public string? CustomerId { get; set; }
    }
}
