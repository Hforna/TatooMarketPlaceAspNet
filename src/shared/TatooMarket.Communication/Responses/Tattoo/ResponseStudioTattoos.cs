using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Responses.Studio;
using TatooMarket.Communication.Responses.Tatto;

namespace TatooMarket.Communication.Responses.Tattoo
{
    public class ResponseStudioTattoos
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public int Count { get; set; }
        public int FirstItemOnPage { get; set; }
        public int LastItemOnPage { get; set; }
        public int TotalItemCount { get; set; }
        public IList<ResponseShortTatto> Tattoos { get; set; } = [];
    }
}
