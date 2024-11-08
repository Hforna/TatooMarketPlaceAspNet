using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.Studio;
using TatooMarket.Communication.Responses.Studio;

namespace TatooMarket.Application.UseCases.Repositories.Studio
{
    public interface ICreateStudio
    {
        public Task<ResponseShortStudio> Execute(RequestCreateStudio request);
    }
}
