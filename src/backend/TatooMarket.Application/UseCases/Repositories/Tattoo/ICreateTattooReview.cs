using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.Tattoo;

namespace TatooMarket.Application.UseCases.Repositories.Tattoo
{
    public interface ICreateTattooReview
    {
        public Task Execute(RequestCreateTattooReview request);
    }
}
