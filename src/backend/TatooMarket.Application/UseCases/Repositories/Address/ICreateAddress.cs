using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.Address;
using TatooMarket.Communication.Responses.Address;

namespace TatooMarket.Application.UseCases.Repositories.Address
{
    public interface ICreateAddress
    {
        public Task<ResponseAddress> Execute(RequestCreateAddress request);
    }
}
