using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.Tattoo;
using TatooMarket.Communication.Responses.Tattoo;

namespace TatooMarket.Application.UseCases.Repositories.Tattoo
{
    public interface IGetWeeksTattoos
    {
        public Task<ResponseWeeksTattoos> Execute(RequestSelectDate request);
    }
}
