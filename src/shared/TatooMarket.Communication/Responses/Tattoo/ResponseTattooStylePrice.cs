using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Enums;

namespace TatooMarket.Communication.Responses.Tattoo
{
    public class ResponseTattooStylePrice
    {
        public string? Id { get; set; }
        public string? StudioId { get; set; }
        public TattooStyleEnum TattooStyle { get; set; }
        public CurrencyEnum CurrencyType { get; set; }
        public float Price { get; set; }
    }
}
