using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Enums;
using TatooMarket.Communication.Responses.Finance;

namespace TatooMarket.Application.UseCases.Repositories.Finance
{
    public interface IBuyTattoo
    {
        public Task<ResponseStripeUrl> Execute();
    }
}
