using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Responses.Tattoo;
using TatooMarket.Domain.Entities.Tattoo;
using X.PagedList;

namespace TatooMarket.Application.UseCases.Repositories.Tattoo
{
    public interface IGetStudioTattoos
    {
        public Task<ResponseStudioTattoos> Execute(long studioId, int pageNumber);
    }
}
