using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.Finance;
using TatooMarket.Communication.Responses.Finance;

namespace TatooMarket.Application.UseCases.Repositories.Finance
{
    public interface ICreateFinanceAccount
    {
        public Task<ResponseCreateFinanceAccount> Execute(RequestCreateFinanceAccount request);
    }
}
