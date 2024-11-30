using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Responses.Tatto;

namespace TatooMarket.Communication.Responses.Studio
{
    public class ResponseFullStudio
    {
        public string StudioName { get; set; }
        public string OwnerId { get; set; }
        public string StudioLogo { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberTattoos { get; set; }
        public IList<ResponseShortTatto> RecentTattoos { get; set; }
    }
}
