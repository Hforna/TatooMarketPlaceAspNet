using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.Login;
using TatooMarket.Communication.Responses.Login;

namespace TatooMarket.Application.UseCases.Repositories.Login
{
    public interface ILoginByApplication
    {
        public Task<ResponseLogin> Execute(RequestLogin request);
    }
}
