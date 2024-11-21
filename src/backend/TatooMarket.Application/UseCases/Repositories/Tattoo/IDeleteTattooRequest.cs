using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Application.UseCases.Repositories.Tattoo
{
    public interface IDeleteTattooRequest
    {
        public Task Execute(long id);
    }
}
