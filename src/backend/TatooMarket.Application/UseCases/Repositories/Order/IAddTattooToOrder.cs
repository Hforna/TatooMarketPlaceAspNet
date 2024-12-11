using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.Order;

namespace TatooMarket.Application.UseCases.Repositories.Order
{
    public interface IAddTattooToOrder
    {
        public Task Execute(RequestAddTattooToOrder request, ISession session);
    }
}
