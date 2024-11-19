using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Responses.Tatto;

namespace TatooMarket.Communication.Responses.Studio
{
    public class ResponseShortStudio
    {
        public string StudioName { get; set; }
        public string OwnerId { get; set; }
        public string ImageStudio {  get; set; }
        public string StudioId { get; set; }
        public int Note {  get; set; }
        public IList<ResponseShortTatto>? RecentTattoss { get; set; }
    }
}
